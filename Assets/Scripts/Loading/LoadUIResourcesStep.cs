using UnityEngine;
using WildGunman.UI;
using WildGunman.UI.Screens;
using WildGunman.UI.Windows;

namespace WildGunman.Loading
{
    public class LoadUIResourcesStep : IGameLoadingStep
    {
        private const string WindowsPath = "UI/Windows";
        private const string ScreensPath = "UI/Screens";
        private readonly UIManager _uiManager;

        public LoadUIResourcesStep(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Load()
        {
            var screens = Resources.LoadAll<UIScreen>(ScreensPath);
            foreach (var screen in screens)
            {
                _uiManager.AddScreenPrefab(screen);
            }

            var windows = Resources.LoadAll<UIWindow>(WindowsPath);
            foreach (var window in windows)
            {
                _uiManager.AddWindowPrefab(window);
            }
        }
    }
}