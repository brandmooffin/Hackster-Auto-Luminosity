package apps.brokenwallsstudios.autoluminosity;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.annotation.TargetApi;
import android.app.ListActivity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;

import apps.brokenwallsstudios.autoluminosity.model.Light;
import apps.brokenwallsstudios.autoluminosity.model.Room;

public class RoomDetailsActivity extends ListActivity {

    AppSettings appSettings;
    Room room;
    List<Light> lights;
    private View mProgressView;
    public static EfficientAdapter LightsAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_room_details);

        appSettings = new AppSettings(this);
        LightsAdapter = new EfficientAdapter(this);

        mProgressView = findViewById(R.id.main_progress);

        room = new Room();
        room.Id = getIntent().getExtras().getInt("roomId");
        room.ExternalId = getIntent().getExtras().getString("roomExternalId");
        room.Name = getIntent().getExtras().getString("roomName");
        room.UserId = appSettings.UserId;


        TextView mLightNameText = (TextView) findViewById(R.id.roomNameText);
        mLightNameText.setText(room.Name);

        Button mAddScheduleButton = (Button) findViewById(R.id.addScheduleButton);
        mAddScheduleButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent mainIntent = new Intent(getBaseContext(),AddScheduleActivity.class);
                mainIntent.putExtra("entityId", room.Id);
                mainIntent.putExtra("scheduleType", 1);
                startActivity(mainIntent);
            }
        });

        Button mToggleLightButton = (Button) findViewById(R.id.toggleRoomButton);
        mToggleLightButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ToggleRoomTask mToggleRoomTask = new ToggleRoomTask();
                mToggleRoomTask.execute((Void) null);
            }
        });

        Button mUnregisterAlarmButton = (Button) findViewById(R.id.unregisterRoomButton);
        mUnregisterAlarmButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                // Unregister device
                //UnregisterDeviceTask mUnregisterDeviceTask = new UnregisterDeviceTask();
                //mUnregisterDeviceTask.execute((Void) null);
            }
        });

    }

    @Override
    public void onResume(){
        super.onResume();

        showProgress(true);
        GetLightsTask mGetLightsTask = new GetLightsTask();
        mGetLightsTask.execute((Void) null);
    }


    public class EfficientAdapter extends BaseAdapter {
        private LayoutInflater mInflater;

        public EfficientAdapter(Context context) {
            // Cache the LayoutInflate to avoid asking for a new one each time.
            mInflater = LayoutInflater.from(context);
        }

        /**
         * The number of items in the list is determined by the number of speeches
         * in our array.
         *
         * @see android.widget.ListAdapter#getCount()
         */
        public int getCount() {
            return (lights.size());
        }

        /**
         * Since the data comes from an array, just returning the index is
         * sufficent to get at the data. If we were using a more complex data
         * structure, we would return whatever object represents one row in the
         * list.
         *
         * @see android.widget.ListAdapter#getItem(int)
         */
        public Object getItem(int position) {
            return position;
        }

        /**
         * Use the array index as a unique id.
         *
         * @see android.widget.ListAdapter#getItemId(int)
         */
        public long getItemId(int position) {
            return position;
        }

        /**
         * Make a view to hold each row.
         *
         * @see android.widget.ListAdapter#getView(int, android.view.View,
         *      android.view.ViewGroup)
         */
        public View getView(int position, View convertView, ViewGroup parent) {
            // When convertView is not null, we can reuse it directly, there is no need
            // to reinflate it. We only inflate a new View when the convertView supplied
            // by ListView is null.
            if (convertView == null) {
                convertView = mInflater.inflate(R.layout.expandable_light_item, null);
            }

            TextView deviceNameText = (TextView) convertView.findViewById(R.id.deviceNameText);
            deviceNameText.setGravity(Gravity.CENTER_VERTICAL | Gravity.LEFT);
            deviceNameText.setTextColor(Color.WHITE);
            deviceNameText.setBackgroundColor(Color.DKGRAY);
            deviceNameText.setMinHeight(64);

            TextView deviceDateText = (TextView) convertView.findViewById(R.id.deviceDateText);
            deviceDateText.setGravity(Gravity.CENTER_VERTICAL | Gravity.LEFT);
            deviceDateText.setTextColor(Color.BLACK);

            Light device = lights.get(position);
            deviceNameText.setText(device.Name);
            deviceDateText.setText("Added: " + device.CreateDate);

            return convertView;
        }
    }

    @TargetApi(Build.VERSION_CODES.HONEYCOMB_MR2)
    public void showProgress(final boolean show) {
        // On Honeycomb MR2 we have the ViewPropertyAnimator APIs, which allow
        // for very easy animations. If available, use these APIs to fade-in
        // the progress spinner.
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB_MR2) {
            int shortAnimTime = getResources().getInteger(android.R.integer.config_shortAnimTime);

            getListView().setVisibility(show ? View.GONE : View.VISIBLE);
            getListView().animate().setDuration(shortAnimTime).alpha(
                    show ? 0 : 1).setListener(new AnimatorListenerAdapter() {
                @Override
                public void onAnimationEnd(Animator animation) {
                    getListView().setVisibility(show ? View.GONE : View.VISIBLE);
                }
            });

            mProgressView.setVisibility(show ? View.VISIBLE : View.GONE);
            mProgressView.animate().setDuration(shortAnimTime).alpha(
                    show ? 1 : 0).setListener(new AnimatorListenerAdapter() {
                @Override
                public void onAnimationEnd(Animator animation) {
                    mProgressView.setVisibility(show ? View.VISIBLE : View.GONE);
                }
            });
        } else {
            // The ViewPropertyAnimator APIs are not available, so simply show
            // and hide the relevant UI components.
            mProgressView.setVisibility(show ? View.VISIBLE : View.GONE);
            getListView().setVisibility(show ? View.GONE : View.VISIBLE);
        }
    }

    public class ToggleRoomTask extends AsyncTask<Void, Void, Boolean> {

        @Override
        protected Boolean doInBackground(Void... params) {
            try {
                try {
                    URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/ToggleRoom?roomId="+room.Id);
                    HttpURLConnection connection = (HttpURLConnection) url.openConnection();
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
                }catch(Exception ex){

                }
                return true;
            } catch (Exception e) {
                System.out.println("\nError while calling service");
                System.out.println(e);
            }
            return false;
        }

        @Override
        protected void onPostExecute(final Boolean success) {
            if (success) {
                Toast.makeText(getBaseContext(), "Room toggled.", Toast.LENGTH_LONG).show();
            }else{
                Toast.makeText(getBaseContext(), "Unable to update the alarm. Please try again later.", Toast.LENGTH_LONG).show();
            }
        }
    }

    public class GetLightsTask extends AsyncTask<Void, Void, Boolean> {

        @Override
        protected Boolean doInBackground(Void... params) {
            try {

                Gson gson = new Gson();

                URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/GetLights?userId="+appSettings.UserId);
                URLConnection connection = url.openConnection();
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
                lights = gson.fromJson(sb.toString(), new TypeToken<List<Light>>(){}.getType());
                if(lights != null){
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

            showProgress(false);

            if (success) {
                // We found devices! Update the List Adapter
                setListAdapter(LightsAdapter);
            }
        }

        @Override
        protected void onCancelled() {
            showProgress(false);
        }
    }
}