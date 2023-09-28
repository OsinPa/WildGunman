namespace WildGunman.Save
{
    public interface ISaveSystem
    {
        public int GetInt(string key);
        public void SetInt(string key, int value);
        public void Save();
    }
}