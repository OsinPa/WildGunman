using System;
using System.Collections.Generic;
using UnityEngine;
using WildGunman.Logger;
using WildGunman.UI.Screens;
using WildGunman.UI.Windows;

namespace WildGunman.UI
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private Transform _screenParent;
        [SerializeField] private Transform _windowParent;

        private static readonly ILog Log = LogManager.GetLogger(typeof(UIManager));
        private readonly Dictionary<Type, UIScreen> _screens = new ();
        private readonly Dictionary<Type, UIWindow> _windows = new ();
        private readonly Stack<UIWindow> _openedWindows = new ();
        private UIScreen _currentScreen;

        public void AddScreenPrefab(UIScreen screenPrefab)
        {
            var screen = Instantiate(screenPrefab, _screenParent);
            screen.Hide();

            _screens.Add(screen.GetType(), screen);
        }

        public void AddWindowPrefab(UIWindow windowPrefab)
        {
            var window = Instantiate(windowPrefab, _windowParent);
            window.Hide();
            window.CloseAction += CloseTopWindow;

            _windows.Add(window.GetType(), window);
        }

        public TScreen ShowScreen<TScreen>() where TScreen : UIScreen
        {
            var screen = GetScreen<TScreen>();
            if (screen != null)
            {
                ShowScreen(screen);
            }
            return screen;
        }

        public TScreen ShowScreen<TScreen, TData>(in TData data) where TScreen : UIDataScreen<TData>
        {
            var screen = GetScreen<TScreen>();
            if (screen != null)
            {
                screen.Init(in data);
                ShowScreen(screen);
            }
            return screen;
        }

        public void ShowWindow<TWindow>() where TWindow : UIWindow
        {
            var window = GetWindow<TWindow>();
            if (window != null)
            {
                ShowWindow(window);
            }
        }

        public void ShowWindow<TWindow, TData>(in TData data) where TWindow : UIDataWindow<TData>
        {
            var window = GetWindow<TWindow>();
            if (window != null)
            {
                window.Init(in data);
                ShowWindow(window);
            }
        }

        public void HideAll()
        {
            if (_currentScreen != null)
            {
                _currentScreen.Hide();
                _currentScreen = null;
            }

            foreach (var window in _openedWindows)
            {
                window.Hide();
            }
            _openedWindows.Clear();
        }

        private TScreen GetScreen<TScreen>() where TScreen : UIScreen
        {
            var screenType = typeof(TScreen);
            if (_screens.TryGetValue(screenType, out var screen))
            {
                return screen as TScreen;
            }

            Log.AddField("Type", screenType).Warn("Screen not found");

            return null;
        }

        private TWindow GetWindow<TWindow>() where TWindow : UIWindow
        {
            var windowType = typeof(TWindow);
            if (_windows.TryGetValue(windowType, out var window))
            {
                return window as TWindow;
            }

            Log.AddField("Type", windowType).Warn("Window not found");

            return null;
        }

        private void ShowScreen(UIScreen screen)
        {
            if (_currentScreen != null)
            {
                return;
            }

            screen.Show();
            _currentScreen = screen;
        }

        private void ShowWindow(UIWindow window)
        {
            window.transform.SetAsLastSibling();
            window.Show();
            _openedWindows.Push(window);
        }

        private void CloseTopWindow()
        {
            if (_openedWindows.Count > 0)
            {
                var window = _openedWindows.Pop();
                window.Hide();
            }
        }
    }
}