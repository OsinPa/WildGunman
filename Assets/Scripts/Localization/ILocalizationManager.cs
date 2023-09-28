namespace WildGunman.Localization
{
    public interface ILocalizationManager
    {
        public string Get(string key);
        public string Get(string key, params object[] args);
    }
}