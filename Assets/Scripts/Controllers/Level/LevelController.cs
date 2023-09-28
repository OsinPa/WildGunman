using System.Collections.Generic;
using WildGunman.Logger;
using WildGunman.Save;

namespace WildGunman.Controllers.Level
{
    public class LevelController : ILevelController
    {
        public IEnumerable<LevelModel> Levels => _levels;
        public LevelModel CurrentLevel { get; private set; }

        private static readonly ILog Log = LogManager.GetLogger(typeof(LevelController));
        private readonly LinkedList<LevelModel> _levels = new ();
        private readonly ISaveSystem _saveSystem;

        public LevelController(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public void AddLevel(LevelConfig config)
        {
            CurrentLevel = new LevelModel(config, _saveSystem);
            _levels.AddLast(CurrentLevel);
        }

        public void ChangeCurrentLevel(LevelModel level)
        {
            if (level == null)
            {
                Log.Warn("Level is null");
                return;
            }

            CurrentLevel = level;
        }
    }
}