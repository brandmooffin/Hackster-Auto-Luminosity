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
import android.widget.Switch;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;

import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

import apps.brokenwallsstudios.autoluminosity.model.Light;

public class LightDetailsActivity extends AppCompatActivity {

    AppSettings appSettings;
    Light light;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_light_details);

        appSettings = new AppSettings(this);

        light = new Light();
        light.Id = getIntent().getExtras().getInt("lightId");
        light.ExternalId = getIntent().getExtras().getString("lightExternalId");
        light.Name = getIntent().getExtras().getString("lightName");
        light.IsOn = getIntent().getExtras().getBoolean("lightIsOn");
        light.UserId = appSettings.UserId;


        TextView mLightNameText = (TextView) findViewById(R.id.lightNameText);
        mLightNameText.setText(light.Name);

        Button mAddScheduleButton = (Button) findViewById(R.id.addScheduleButton);
        mAddScheduleButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent mainIntent = new Intent(getBaseContext(), AddScheduleActivity.class);
                mainIntent.putExtra("entityId", light.Id);
                mainIntent.putExtra("scheduleType", 2);
                startActivity(mainIntent);
            }
        });

        Button mToggleLightButton = (Button) findViewById(R.id.toggleLightButton);
        mToggleLightButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                light.IsOn = !light.IsOn;

                // update alarm
                UpdateLightTask mUpdateLightTask = new UpdateLightTask();
                mUpdateLightTask.execute((Void) null);
            }
        });

        Button mUnregisterAlarmButton = (Button) findViewById(R.id.unregisterLightButton);
        mUnregisterAlarmButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                // Unregister device
                //UnregisterDeviceTask mUnregisterDeviceTask = new UnregisterDeviceTask();
                //mUnregisterDeviceTask.execute((Void) null);
            }
        });

    }

    public class UpdateLightTask extends AsyncTask<Void, Void, Boolean> {

        @Override
        protected Boolean doInBackground(Void... params) {
            try {
                URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/UpdateLight");
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
                try {
                    int lightAction = 1;
                    if(!light.IsOn){
                        lightAction = 2;
                    }
                    URL url = new URL("http://autoluminosityservice.cloudapp.net/AutoLuminosityService/ToggleLight?lightId="+light.Id+"&action="+lightAction);
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
                Toast.makeText(getBaseContext(), "Light toggled.", Toast.LENGTH_LONG).show();
            }else{
                Toast.makeText(getBaseContext(), "Unable to update the alarm. Please try again later.", Toast.LENGTH_LONG).show();
            }
        }
    }

}
