using System.Collections.Generic;
using Newtonsoft.Json;

namespace WildGunman.Localization
{
    public class LocalizationConfig
    {
        public string Language => _language;
        public Dictionary<string, string> Locales => _locales;

        [JsonProperty("language")] private string _language;
        [JsonProperty("locales")] private Dictionary<string, string> _locales;
    }
}