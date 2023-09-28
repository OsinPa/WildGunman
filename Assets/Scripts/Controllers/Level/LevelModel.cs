using WildGunman.Save;

namespace WildGunman.Controllers.Level
{
    public class LevelModel
    {
        public int BestScore { get; private set; }
        public LevelConfig Config { get; }

        private readonly ISaveSystem _saveSystem;

        public LevelModel(LevelConfig config, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            Config = config;
            BestScore = _saveSystem.GetInt(Config.Id);
        }

        public void TryUpdateBestScore(int score)
        {
            if (BestScore < score)
            {
                BestScore = score;
                _saveSystem.SetInt(Config.Id, BestScore);
            }
        }
    }
}