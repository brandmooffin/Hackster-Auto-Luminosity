package apps.brokenwallsstudios.autoluminosity;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;

/**
 * Created by brand on 1/9/2016.
 */
public class AppSettings {
    public static String AppSettingsName = "AutoLuminositySettings";

    public static String UserIdSetting = "UserId";
    public static String UserEmailSetting = "UserEmail";

    public static String LifxPersonalAccessToken = "c4820c66d34488233d27d0afde2ae27c43960748dc7fce7c19cdc29137bd3d52";

    public int UserId = 0;
    public String UserEmail = null;

    private SharedPreferences AppPreferences;

    public AppSettings(Context context){
        AppPreferences = context.getSharedPreferences(AppSettingsName, Activity.MODE_PRIVATE);
        LoadSettings();
    }

    public void SaveSettings(){
        SharedPreferences.Editor editor = AppPreferences.edit();
        editor.putInt(UserIdSetting, UserId);
        editor.putString(UserEmailSetting, UserEmail);
        editor.commit();
    }

    public void LoadSettings(){
        UserId = AppPreferences.getInt(UserIdSetting, 0);
        UserEmail = AppPreferences.getString(UserEmailSetting, null);
    }
}
