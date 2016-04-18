using Windows.Storage;

namespace AutoLuminosityIotApp
{
    public class AppSettings
    {
        #region Fields
        public int UserId = 0;
        public string UserEmail = string.Empty;

        #endregion

        #region Initialzations
        public AppSettings()
        {
            LoadSettings();
        }

        #endregion

        public void RestoreSettingsToDefaults()
        {
            UserId = 0;
            UserEmail = string.Empty;
        }

        public void SaveSettings()
        {
            ApplicationData.Current.LocalSettings.Values["UserId"] = UserId;
            ApplicationData.Current.LocalSettings.Values["UserEmail"] = UserEmail;
        }

        public void LoadSettings()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("UserId"))
            {
                UserId = 1; //(int)ApplicationData.Current.LocalSettings.Values["UserId"];
            }
            
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("UserEmail"))
            {
                UserEmail = ApplicationData.Current.LocalSettings.Values["UserEmail"].ToString();
            }
        }
    }
}
