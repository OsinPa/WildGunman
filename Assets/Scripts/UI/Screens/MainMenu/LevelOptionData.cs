using System;
using TMPro;
using WildGunman.Controllers.Level;

namespace WildGunman.UI.Screens.MainMenu
{
    public class LevelOptionData : TMP_Dropdown.OptionData, IComparable
    {
        public readonly LevelModel Level;

        public LevelOptionData(string name, LevelModel level) : base(name)
        {
            Level = level;
        }

        public int CompareTo(object obj)
        {
            if (obj is LevelOptionData levelOptionData)
            {
                return Level.Config.Order.CompareTo(levelOptionData.Level.Config.Order);
            }

            return 1;
        }
    }
}