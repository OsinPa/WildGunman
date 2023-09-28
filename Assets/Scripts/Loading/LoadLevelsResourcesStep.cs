using Newtonsoft.Json;
using UnityEngine;
using WildGunman.Controllers.Level;

namespace WildGunman.Loading
{
    public class LoadLevelsResourcesStep : IGameLoadingStep
    {
        private const string LevelConfigsPath = "Levels/Configs";
        private readonly LevelController _levelController;

        public LoadLevelsResourcesStep(LevelController levelController)
        {
            _levelController = levelController;
        }

        public void Load()
        {
            var textAssets = Resources.LoadAll<TextAsset>(LevelConfigsPath);
            foreach (var textAsset in textAssets)
            {
                var config = JsonConvert.DeserializeObject<LevelConfig>(textAsset.text);
                if (config != null)
                {
                    _levelController.AddLevel(config);
                }
            }
        }
    }
}