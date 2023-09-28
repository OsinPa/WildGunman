using UnityEngine;
using UnityEngine.SceneManagement;
using WildGunman.Controllers.Level;
using WildGunman.Loading;
using WildGunman.Localization;
using WildGunman.Save;
using WildGunman.UI;

namespace WildGunman
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }
        public IUIManager UIManager => _uiManager;
        public ILevelController LevelController => _levelController;
        public ILocalizationManager LocalizationManager => _localizationManager;

        [SerializeField] private UIManager _uiManager;

        private ISaveSystem _saveSystem;
        private LevelController _levelController;
        private LocalizationManager _localizationManager;

        public static void LoadScene(GameScene scene)
        {
            SceneManager.LoadScene((int) scene);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Init();
            }
            else
            {
                Destroy(gameObject);
            }            
        }

        private void Init()
        {
            _saveSystem = new PlayerPrefsSaveSystem();
            _levelController = new LevelController(_saveSystem);
            _localizationManager = new LocalizationManager();

            LoadResources();

            LoadScene(GameScene.MainMenu);
            SceneManager.sceneUnloaded += SceneUnloadedHandler;
        }

        private void LoadResources()
        {
            new LoadUIResourcesStep(_uiManager).Load();
            new LoadLevelsResourcesStep(_levelController).Load();
            new LoadLocalizationResourcesStep(_localizationManager).Load();
        }

        private void SceneUnloadedHandler(Scene scene)
        {
            _uiManager.HideAll();
        }
    }

    public enum GameScene
    {
        MainMenu = 1,
        Level
    }
}