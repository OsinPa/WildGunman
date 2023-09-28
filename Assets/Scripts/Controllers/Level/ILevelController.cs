using System.Collections.Generic;

namespace WildGunman.Controllers.Level
{
    public interface ILevelController
    {
        public IEnumerable<LevelModel> Levels { get; }
        public LevelModel CurrentLevel { get; }

        public void ChangeCurrentLevel(LevelModel level);
    }
}