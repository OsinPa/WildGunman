using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using WildGunman.Localization;

namespace WildGunman.Loading
{
    public class LoadLocalizationResourcesStep : IGameLoadingStep
    {
        private const string LocalizationPath = "Localization";
        private const string DefaultLanguage = "en";
        private readonly Dictionary<SystemLanguage, string> _languages = new()
        {
            { SystemLanguage.English, DefaultLanguage },
            { SystemLanguage.Russian, "ru" }
        };
        private readonly LocalizationManager _localizationManager;

        public LoadLocalizationResourcesStep(LocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        public void Load()
        {
            LocalizationConfig config = null;
            if (_languages.TryGetValue(Application.systemLanguage, out var language))
            {
                config = LoadConfig($"{LocalizationPath}/{language}");
            }

            config ??= LoadConfig($"{LocalizationPath}/{DefaultLanguage}");
            _localizationManager.SetConfig(config);
        }

        private static LocalizationConfig LoadConfig(string path)
        {
            var textAsset = Resources.Load<TextAsset>(path);
            if (textAsset != null)
            {
                return JsonConvert.DeserializeObject<LocalizationConfig>(textAsset.text);
            }

            return null;
        }
    }
}