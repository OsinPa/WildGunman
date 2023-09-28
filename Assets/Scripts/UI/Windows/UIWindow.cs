using System;
using WildGunman.UI.Screens;

namespace WildGunman.UI.Windows
{
    public abstract class UIWindow : UIScreen
    {
        public event Action CloseAction;

        protected void Close()
        {
            CloseAction?.Invoke();
        }
    }
}