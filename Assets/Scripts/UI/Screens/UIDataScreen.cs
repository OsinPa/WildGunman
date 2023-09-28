namespace WildGunman.UI.Screens
{
    public abstract class UIDataScreen<TData> : UIScreen
    {
        public abstract void Init(in TData data);
    }
}