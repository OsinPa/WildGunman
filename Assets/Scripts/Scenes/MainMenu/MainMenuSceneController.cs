using UnityEngine;
using WildGunman.Controllers.Level;
using WildGunman.UI.Screens.MainMenu;

namespace WildGunman.Scenes.MainMenu
{
    public class MainMenuSceneController : MonoBehaviour
    {
        private void Awake()
        {
            var screenData = new MainMenuScreen.Data
            {
                Levels = Game.Instance.LevelController.Levels,
                LocalizationManager = Game.Instance.LocalizationManager,
                PlayLevelCallback = MainMenuScreenPlayLevelHandler
            };
            Game.Instance.UIManager.ShowScreen<MainMenuScreen, MainMenuScreen.Data>(in screenData);
        }

        private static void MainMenuScreenPlayLevelHandler(LevelModel level)
        {
            Game.Instance.LevelController.ChangeCurrentLevel(level);
            Game.LoadScene(GameScene.Level);
        }
    }
}