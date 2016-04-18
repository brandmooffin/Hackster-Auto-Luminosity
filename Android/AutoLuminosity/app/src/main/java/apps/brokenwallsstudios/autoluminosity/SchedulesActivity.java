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
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;

import apps.brokenwallsstudios.autoluminosity.model.Light;
import apps.brokenwallsstudios.autoluminosity.model.Room;
import apps.brokenwallsstudios.autoluminosity.model.Schedule;

public class SchedulesActivity extends ListActivity {

    public static List<Schedule> schedules;
    AppSettings appSettings;

    private View mProgressView;
    public static EfficientAdapter SchedulesAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lights);

        appSettings = new AppSettings(this);
        SchedulesAdapter = new EfficientAdapter(this);

        mProgressView = findViewById(R.id.main_progress);
    }

    @Override
    public void onResume(){
        super.onResume();

        showProgress(true);
        GetRoomsTask mGetRoomsTask = new GetRoomsTask();
        mGetRoomsTask.execute((Void) null);
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        //getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        //if (id == R.id.action_settings) {
        //  return true;
        //}

        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onListItemClick(ListView l, View v, int position, long id) {
        Schedule schedule = schedules.get(position);
        Intent mainIntent = new Intent(getBaseContext(),ScheduleDetailsActivity.class);
        mainIntent.putExtra("scheduleId", schedule.Id);
        mainIntent.putExtra("scheduleEntityName", schedule.EntityName);
        mainIntent.putExtra("scheduleExecuteTime", schedule.ExecuteTime);
        mainIntent.putExtra("scheduleAction", schedule.Action);
        startActivity(mainIntent);
    }

    /**
     * Shows the progress UI and hides the login form.
     */
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
            return (schedules.size());
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

            Schedule schedule = schedules.get(position);
            deviceNameText.setText(schedule.EntityName + " @ " + schedule.ExecuteTime);
            deviceDateText.setText("Action: On");
            if(schedule.Action == 2){
                deviceDateText.setText("Action: Off");
            }

            return convertView;
        }
    }

    public class GetRoomsTask extends AsyncTask<Void, Void, Boolean> {

        @Override
        protected Boolean doInBackground(Void... params) {
            try {

                Gson gson = new Gson();

                URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/GetSchedulesForUser?userId="+appSettings.UserId);
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
                schedules = gson.fromJson(sb.toString(), new TypeToken<List<Schedule>>(){}.getType());
                if(schedules != null){
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
                setListAdapter(SchedulesAdapter);
            }
        }

        @Override
        protected void onCancelled() {
            showProgress(false);
        }
    }
}