package apps.brokenwallsstudios.autoluminosity;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.annotation.TargetApi;
import android.app.ListActivity;
import android.app.LoaderManager;
import android.content.Context;
import android.content.Intent;
import android.content.Loader;
import android.database.Cursor;
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
import android.view.Menu;
import android.view.MenuItem;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    AppSettings appSettings;

    private View mProgressView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        appSettings = new AppSettings(this);

        Button mLightsButton = (Button) findViewById(R.id.lightButton);
        mLightsButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent activityIntent = new Intent(getBaseContext(), LightsActivity.class);
                startActivity(activityIntent);
            }
        });

        Button mRoomsButton = (Button) findViewById(R.id.roomButton);
        mRoomsButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent activityIntent = new Intent(getBaseContext(), RoomsActivity.class);
                startActivity(activityIntent);
            }
        });

        Button mSchedulesButton = (Button) findViewById(R.id.scheduleButton);
        mSchedulesButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent activityIntent = new Intent(getBaseContext(), SchedulesActivity.class);
                startActivity(activityIntent);
            }
        });

        Button mSettingsButton = (Button) findViewById(R.id.settingsButton);
        mSettingsButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent activityIntent = new Intent(getBaseContext(), SettingsActivity.class);
                startActivity(activityIntent);
            }
        });

        mProgressView = findViewById(R.id.main_progress);
    }
}