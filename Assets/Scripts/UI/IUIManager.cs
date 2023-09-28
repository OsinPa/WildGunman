using WildGunman.UI.Screens;
using WildGunman.UI.Windows;

namespace WildGunman.UI
{
    public interface IUIManager
    {
        public TScreen ShowScreen<TScreen>() where TScreen : UIScreen;
        public TScreen ShowScreen<TScreen, TData>(in TData data) where TScreen : UIDataScreen<TData>;
        public void ShowWindow<TWindow>() where TWindow : UIWindow;
        public void ShowWindow<TWindow, TData>(in TData data) where TWindow : UIDataWindow<TData>;
    }
}