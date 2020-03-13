using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace FriskaClient.Services
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
{
    private static ISettings AppSettings
    {
        get
        {
            return CrossSettings.Current;
        }
    }

    #region Setting Constants

    private const string LastEmailSettingsKey = "last_email_key";  
    private const string LastPasswordKey = "last_password_key";
    private static readonly string SettingsDefault = string.Empty;

    #endregion


    public static string LastUsedEmail
    {
        get
        {
            return AppSettings.GetValueOrDefault(LastEmailSettingsKey, SettingsDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue(LastEmailSettingsKey, value);
        }
    }
        public static string LastUsedPassword
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastPasswordKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastPasswordKey, value);
            }
        }

    }
}
