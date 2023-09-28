using WildGunman.Logger;

namespace WildGunman.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LocalizationManager));
        private LocalizationConfig _config;

        public void SetConfig(LocalizationConfig config)
        {
            _config = config;
        }

        public string Get(string key)
        {
            if (_config.Locales.TryGetValue(key, out var value))
            {
                return value;
            }

            Log.AddField("Language", _config.Language).AddField("Key", key).Warn("No localization");

            return key;
        }

        public string Get(string key, params object[] args)
        {
            return string.Format(Get(key), args);
        }
    }
}