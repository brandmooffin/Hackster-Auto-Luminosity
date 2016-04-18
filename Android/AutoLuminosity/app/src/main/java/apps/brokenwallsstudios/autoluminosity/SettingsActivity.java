package apps.brokenwallsstudios.autoluminosity;


import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.content.res.Configuration;
import android.media.Ringtone;
import android.media.RingtoneManager;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.preference.ListPreference;
import android.preference.Preference;
import android.preference.PreferenceActivity;
import android.support.v7.app.ActionBar;
import android.preference.PreferenceFragment;
import android.preference.PreferenceManager;
import android.preference.RingtonePreference;
import android.text.TextUtils;
import android.view.MenuItem;
import android.widget.Toast;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;

import apps.brokenwallsstudios.autoluminosity.model.LifxGroup;
import apps.brokenwallsstudios.autoluminosity.model.LifxLight;
import apps.brokenwallsstudios.autoluminosity.model.Light;
import apps.brokenwallsstudios.autoluminosity.model.Room;

/**
 * A {@link PreferenceActivity} that presents a set of application settings. On
 * handset devices, settings are presented as a single list. On tablets,
 * settings are split by category, with category headers shown to the left of
 * the list of settings.
 * <p/>
 * See <a href="http://developer.android.com/design/patterns/settings.html">
 * Android Design: Settings</a> for design guidelines and the <a
 * href="http://developer.android.com/guide/topics/ui/settings.html">Settings
 * API Guide</a> for more information on developing a Settings UI.
 */
public class SettingsActivity extends AppCompatPreferenceActivity {
    /**
     * A preference value change listener that updates the preference's summary
     * to reflect its new value.
     */
    private static Preference.OnPreferenceChangeListener sBindPreferenceSummaryToValueListener = new Preference.OnPreferenceChangeListener() {
        @Override
        public boolean onPreferenceChange(Preference preference, Object value) {
            String stringValue = value.toString();

            if (preference instanceof ListPreference) {
                // For list preferences, look up the correct display value in
                // the preference's 'entries' list.
                ListPreference listPreference = (ListPreference) preference;
                int index = listPreference.findIndexOfValue(stringValue);

                // Set the summary to reflect the new value.
                preference.setSummary(
                        index >= 0
                                ? listPreference.getEntries()[index]
                                : null);

            } else {
                // For all other preferences, set the summary to the value's
                // simple string representation.
                preference.setSummary(stringValue);
            }
            return true;
        }
    };

    /**
     * Helper method to determine if the device has an extra-large screen. For
     * example, 10" tablets are extra-large.
     */
    private static boolean isXLargeTablet(Context context) {
        return (context.getResources().getConfiguration().screenLayout
                & Configuration.SCREENLAYOUT_SIZE_MASK) >= Configuration.SCREENLAYOUT_SIZE_XLARGE;
    }

    /**
     * Binds a preference's summary to its value. More specifically, when the
     * preference's value is changed, its summary (line of text below the
     * preference title) is updated to reflect the value. The summary is also
     * immediately updated upon calling this method. The exact display format is
     * dependent on the type of preference.
     *
     * @see #sBindPreferenceSummaryToValueListener
     */
    private static void bindPreferenceSummaryToValue(Preference preference) {
        // Set the listener to watch for value changes.
        preference.setOnPreferenceChangeListener(sBindPreferenceSummaryToValueListener);

        // Trigger the listener immediately with the preference's
        // current value.
        sBindPreferenceSummaryToValueListener.onPreferenceChange(preference,
                PreferenceManager
                        .getDefaultSharedPreferences(preference.getContext())
                        .getString(preference.getKey(), ""));
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setupActionBar();
    }

    /**
     * Set up the {@link android.app.ActionBar}, if the API is available.
     */
    private void setupActionBar() {
        ActionBar actionBar = getSupportActionBar();
        if (actionBar != null) {
            // Show the Up button in the action bar.
            actionBar.setDisplayHomeAsUpEnabled(true);
        }
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public boolean onIsMultiPane() {
        return isXLargeTablet(this);
    }

    /**
     * {@inheritDoc}
     */
    @Override
    @TargetApi(Build.VERSION_CODES.HONEYCOMB)
    public void onBuildHeaders(List<Header> target) {
        loadHeadersFromResource(R.xml.pref_headers, target);
    }

    /**
     * This method stops fragment injection in malicious applications.
     * Make sure to deny any unknown fragments here.
     */
    protected boolean isValidFragment(String fragmentName) {
        return PreferenceFragment.class.getName().equals(fragmentName)
                || GeneralPreferenceFragment.class.getName().equals(fragmentName)
                || LifxPreferenceFragment.class.getName().equals(fragmentName);
    }

    /**
     * This fragment shows general preferences only. It is used when the
     * activity is showing a two-pane settings UI.
     */
    @TargetApi(Build.VERSION_CODES.HONEYCOMB)
    public static class GeneralPreferenceFragment extends PreferenceFragment {
        @Override
        public void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            addPreferencesFromResource(R.xml.pref_general);
            setHasOptionsMenu(true);

            // Bind the summaries of EditText/List/Dialog/Ringtone preferences
            // to their values. When their values change, their summaries are
            // updated to reflect the new value, per the Android Design
            // guidelines.
            bindPreferenceSummaryToValue(findPreference("example_text"));
            bindPreferenceSummaryToValue(findPreference("example_list"));
        }

        @Override
        public boolean onOptionsItemSelected(MenuItem item) {
            int id = item.getItemId();
            if (id == android.R.id.home) {
                startActivity(new Intent(getActivity(), SettingsActivity.class));
                return true;
            }
            return super.onOptionsItemSelected(item);
        }
    }

    @TargetApi(Build.VERSION_CODES.HONEYCOMB)
    public static class LifxPreferenceFragment extends PreferenceFragment {

        List<LifxLight> Lights = new ArrayList<>();
        AppSettings appSettings;

        @Override
        public void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            addPreferencesFromResource(R.xml.pref_lifx);
            setHasOptionsMenu(true);

            appSettings = new AppSettings(getActivity());
            // Bind the summaries of EditText/List/Dialog/Ringtone preferences
            // to their values. When their values change, their summaries are
            // updated to reflect the new value, per the Android Design
            // guidelines.
            bindPreferenceSummaryToValue(findPreference("access_token"));

            Preference button = findPreference("import_lifx");
            button.setOnPreferenceClickListener(new Preference.OnPreferenceClickListener() {
                @Override
                public boolean onPreferenceClick(Preference preference) {
                    GetLifxDataTask importLifxDataTask = new GetLifxDataTask();
                    importLifxDataTask.execute();
                    return true;
                }
            });
        }

        @Override
        public boolean onOptionsItemSelected(MenuItem item) {
            int id = item.getItemId();
            if (id == android.R.id.home) {
                startActivity(new Intent(getActivity(), SettingsActivity.class));
                return true;
            }
            return super.onOptionsItemSelected(item);
        }

        public class GetLifxDataTask extends AsyncTask<Void, Void, Boolean> {

            @Override
            protected Boolean doInBackground(Void... params) {
                try {

                    Gson gson = new Gson();
                    Lights = new ArrayList<>();

                    URL url = new URL("https://api.lifx.com/v1/lights/");
                    URLConnection connection = url.openConnection();
                    connection.setRequestProperty("Authorization", "Bearer " + AppSettings.LifxPersonalAccessToken);
                    connection.setRequestProperty("Content-Type", "application/json");
                    connection.setConnectTimeout(5000);

                    connection.setReadTimeout(5000);

                    BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
                    StringBuilder sb = new StringBuilder();
                    String line;
                    while ((line = in.readLine()) != null) {
                        sb.append(line);
                    }
                    in.close();
                    Lights = gson.fromJson(sb.toString(), new TypeToken<List<LifxLight>>(){}.getType());
                    if(Lights != null){
                        return true;
                    }
                } catch (Exception e) {
                    System.out.println("\nError while calling service");
                    System.out.println(e);
                }
                return false;
            }

            @Override
            protected void onPostExecute(final Boolean success) {
                if (success) {
                    AddLifxDataTask importLifxDataTask = new AddLifxDataTask();
                    importLifxDataTask.execute();
                }else{
                    Toast.makeText(getContext(), "Unable to import lights from Lifx. Please try again later.", Toast.LENGTH_LONG).show();
                }
            }
        }

        public class AddLifxDataTask extends AsyncTask<Void, Void, Boolean> {

            @Override
            protected Boolean doInBackground(Void... params) {
                try {
                    for (LifxLight _light : Lights) {

                        Light light = new Light();
                        Room room = new Room();
                        LifxGroup group = _light.group;
                        light.ExternalId = _light.id;
                        light.Name = _light.label;
                        light.UserId = appSettings.UserId;
                        light.IsOn = false;
                        if(_light.power.equalsIgnoreCase("on")){
                            light.IsOn = true;
                        }

                        // add light
                        light = AddLight(light);

                        if(group != null && !group.id.isEmpty()){
                            // add group
                            room.ExternalId = group.id;
                            room.Name = group.name;
                            room.UserId = appSettings.UserId;

                            room = AddRoom(room);
                        }

                        if(light.Id > 0 && room.Id > 0){
                            // add link
                            if(!AddLightToRoom(room.Id, light.Id)){
                                // something went wrong
                            }
                        }
                    }
                    return true;
                } catch (Exception e) {
                    System.out.println("\nError while calling service");
                    System.out.println(e);
                }
                return false;
            }

            private Light AddLight(Light light){

                try {
                    URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/AddLight");
                    HttpURLConnection connection = (HttpURLConnection)url.openConnection();
                    connection.setRequestMethod("POST");
                    connection.setRequestProperty("Content-Type", "application/json");
                    connection.setConnectTimeout(5000);
                    connection.setReadTimeout(5000);

                    Gson gson = new Gson();
                    DataOutputStream wr = new DataOutputStream(connection.getOutputStream ());
                    String parsed = gson.toJson(light, Light.class);

                    wr.writeBytes(parsed);

                    wr.flush();
                    wr.close();

                    BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
                    StringBuilder sb = new StringBuilder();
                    String line;
                    while ((line = in.readLine()) != null) {
                        sb.append(line);
                    }
                    in.close();

                    light = gson.fromJson(sb.toString(), Light.class);

                    return light;
                } catch (Exception e) {
                    System.out.println("\nError while calling service");
                    System.out.println(e);
                }
                return light;
            }

            private Room AddRoom(Room room)
            {
                try {
                    URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/AddRoom");
                    HttpURLConnection connection = (HttpURLConnection)url.openConnection();
                    connection.setRequestMethod("POST");
                    connection.setRequestProperty("Content-Type", "application/json");
                    connection.setConnectTimeout(5000);
                    connection.setReadTimeout(5000);

                    Gson gson = new Gson();
                    DataOutputStream wr = new DataOutputStream(connection.getOutputStream ());
                    String parsed = gson.toJson(room, Room.class);

                    wr.writeBytes(parsed);

                    wr.flush();
                    wr.close();

                    BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
                    StringBuilder sb = new StringBuilder();
                    String line;
                    while ((line = in.readLine()) != null) {
                        sb.append(line);
                    }
                    in.close();

                    room = gson.fromJson(sb.toString(), Room.class);

                    return room;
                } catch (Exception e) {
                    System.out.println("\nError while calling service");
                    System.out.println(e);
                }

                return room;
            }

            private boolean AddLightToRoom(int roomId, int lightId)
            {
                try {
                    URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/AddLightToRoom?roomId="+roomId + "&lightId=" + lightId);
                    HttpURLConnection connection = (HttpURLConnection)url.openConnection(); 
                    connection.setRequestProperty("Content-Type", "application/json");
                    connection.setConnectTimeout(5000);
                    connection.setReadTimeout(5000);

                    BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
                    StringBuilder sb = new StringBuilder();
                    String line;
                    while ((line = in.readLine()) != null) {
                        sb.append(line);
                    }
                    in.close();

                    boolean result = Boolean.parseBoolean(sb.toString());
                    return result;
                } catch (Exception e) {
                    System.out.println("\nError while calling service");
                    System.out.println(e);
                }

                return false;
            }

            @Override
            protected void onPostExecute(final Boolean success) {
                if (success) {
                    Toast.makeText(getActivity(), "Finished importing Lifx Lights.", Toast.LENGTH_LONG).show();
                }else{
                    //\Toast.makeText(getBaseContext(), "Unable to unregister the alarm. Please try again later.", Toast.LENGTH_LONG).show();
                }
            }
        }
    }
}
