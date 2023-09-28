using UnityEngine;

namespace WildGunman.Save
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        public int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }
    }
}