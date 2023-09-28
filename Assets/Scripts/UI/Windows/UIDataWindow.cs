namespace WildGunman.UI.Windows
{
    public abstract class UIDataWindow<TData> : UIWindow
    {
        public abstract void Init(in TData data);
    }
}