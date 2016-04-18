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
import android.widget.RadioButton;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;

import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import apps.brokenwallsstudios.autoluminosity.model.LifxGroup;
import apps.brokenwallsstudios.autoluminosity.model.LifxLight;
import apps.brokenwallsstudios.autoluminosity.model.Light;
import apps.brokenwallsstudios.autoluminosity.model.Room;
import apps.brokenwallsstudios.autoluminosity.model.Schedule;

public class AddScheduleActivity extends AppCompatActivity {

    TimePicker FromTimePicker;
    boolean TurnLightsOn = false;
    int EntityId;
    int ScheduleType;
    String ExecuteTime;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_schedule);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        EntityId = getIntent().getExtras().getInt("entityId");
        ScheduleType = getIntent().getExtras().getInt("scheduleType");

        FromTimePicker = (TimePicker) findViewById(R.id.fromTimePicker);

        Button mSaveButton = (Button) findViewById(R.id.saveScheduleButton);
        mSaveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                ExecuteTime = FromTimePicker.getHour() +":"+FromTimePicker.getMinute();

                // add schedule
                AddScheduleTask addScheduleTask = new AddScheduleTask();
                addScheduleTask.execute();
            }
        });
    }

    public void onRadioButtonClicked(View view) {
        boolean checked = ((RadioButton) view).isChecked();
        switch(view.getId()) {
            case R.id.turnLightOnRadio:
                if (checked)
                    TurnLightsOn = true;
                    break;
            case R.id.turnLightOffRadio:
                if (checked)
                    TurnLightsOn = false;
                    break;
        }
    }

    public class AddScheduleTask extends AsyncTask<Void, Void, Boolean> {

        @Override
        protected Boolean doInBackground(Void... params) {
            try {
                try {
                    Schedule schedule = new Schedule();
                    schedule.EntityId = EntityId;
                    schedule.Type = ScheduleType;
                    schedule.Action = 2;
                    schedule.ExecuteTime = ExecuteTime;
                    if(TurnLightsOn){
                        schedule.Action = 1;
                    }

                    URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/AddSchedule");
                    HttpURLConnection connection = (HttpURLConnection)url.openConnection();
                    connection.setRequestMethod("POST");
                    connection.setRequestProperty("Content-Type", "application/json");
                    connection.setConnectTimeout(5000);
                    connection.setReadTimeout(5000);

                    Gson gson = new Gson();
                    DataOutputStream wr = new DataOutputStream(connection.getOutputStream ());
                    String parsed = gson.toJson(schedule, Schedule.class);

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

                    schedule = gson.fromJson(sb.toString(), Schedule.class);

                    return schedule.Id > 0;
                } catch (Exception e) {
                    System.out.println("\nError while calling service");
                    System.out.println(e);
                }
                return false;
            } catch (Exception e) {
                System.out.println("\nError while calling service");
                System.out.println(e);
            }
            return false;
        }

        @Override
        protected void onPostExecute(final Boolean success) {
            if (success) {
                Toast.makeText(getBaseContext(), "Added schedule.", Toast.LENGTH_LONG).show();
            }else{
                Toast.makeText(getBaseContext(), "Unable to add schedule. Please try again later.", Toast.LENGTH_LONG).show();
            }
        }
    }
}
