package apps.brokenwallsstudios.autoluminosity;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;

import apps.brokenwallsstudios.autoluminosity.model.Light;
import apps.brokenwallsstudios.autoluminosity.model.Schedule;

public class ScheduleDetailsActivity extends AppCompatActivity {

    AppSettings appSettings;
    Schedule schedule;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_schedule_details);
        appSettings = new AppSettings(this);

        schedule = new Schedule();
        schedule.Id = getIntent().getExtras().getInt("scheduleId");
        schedule.EntityName = getIntent().getExtras().getString("scheduleEntityName");
        schedule.ExecuteTime = getIntent().getExtras().getString("scheduleExecuteTime");
        schedule.Action = getIntent().getExtras().getInt("scheduleAction");

        TextView mScheduleDescriptionText = (TextView) findViewById(R.id.scheduleDescriptionText);
        mScheduleDescriptionText.setText(schedule.EntityName + " @ " + schedule.ExecuteTime);

        TextView mScheduleActionText = (TextView) findViewById(R.id.scheduleActionText);
        mScheduleActionText.setText("Action: On");
        if(schedule.Action == 2){
            mScheduleActionText.setText("Action: Off");
        }

        Button mUnregisterScheduleButton = (Button) findViewById(R.id.unregisterScheduleButton);
        mUnregisterScheduleButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                UnregisterScheduleTask mUnregisterScheduleTask = new UnregisterScheduleTask();
                mUnregisterScheduleTask.execute((Void) null);
            }
        });
    }

    public class UnregisterScheduleTask extends AsyncTask<Void, Void, Boolean> {

        @Override
        protected Boolean doInBackground(Void... params) {
            try {
                URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/RemoveSchedule?scheduleId="+schedule.Id);
                URLConnection connection = url.openConnection();
                connection.setRequestProperty("Content-Type", "application/json");
                connection.setConnectTimeout(5000);
                connection.setReadTimeout(5000);

                BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
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
                finish();
            }
        }
    }
}
