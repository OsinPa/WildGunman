namespace WildGunman.Logger
{
    public interface ILog
    {
        public ILog AddField(string key, object value);
        public void Warn(object message);
    }
}