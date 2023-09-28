using System.Collections;
using UnityEngine;
using WildGunman.Controllers.Level;
using WildGunman.Logger;
using WildGunman.UI.Screens.Level;
using WildGunman.UI.Windows.GameOver;

namespace WildGunman.Scenes.Level
{
    public class LevelSceneController : MonoBehaviour
    {
        [SerializeField] private LevelContent _defaultContentPrefab;

        private static readonly ILog Log = LogManager.GetLogger(typeof(LevelSceneController));
        private const float ShowGameOverWindowDelaySec = 2f;
        private const string EnemyFireLockKey = "enemy_fire";
        private const string CivilianDeathLockKey = "civilian_death";
        private LevelModel _level;
        private LevelGameLogic _gameLogic;
        private LevelScreen _screen;

        private void Awake()
        {
            _level = Game.Instance.LevelController.CurrentLevel;

            var content = LoadContent(_level.Config.ResourcesPrefabPath);
            _gameLogic = new LevelGameLogic(_level.Config.Characters, content);
            _gameLogic.ScoreChangedAction += ScoreChangedHandler;
            _gameLogic.FinishGameAction += FinishGameHandler;

            var screenData = new LevelScreen.Data
            {
                LocalizationManager = Game.Instance.LocalizationManager
            };
            _screen = Game.Instance.UIManager.ShowScreen<LevelScreen, LevelScreen.Data>(in screenData);
        }

        private void Update()
        {
            _gameLogic.Update(Time.deltaTime);
        }

        private LevelContent LoadContent(string path)
        {
            var contentPrefab = Resources.Load<LevelContent>(path);
            if (contentPrefab == null)
            {
                contentPrefab = _defaultContentPrefab;
                Log.AddField("Path", path).Warn("Prefab was not loaded from Resources");
            }

            return Instantiate(contentPrefab, transform);
        }

        private void OnDestroy()
        {
            _gameLogic.ScoreChangedAction -= ScoreChangedHandler;
            _gameLogic.FinishGameAction -= FinishGameHandler;
        }

        private void ScoreChangedHandler(int score)
        {
            _screen.UpdateScore(_gameLogic.Score);
        }

        private void FinishGameHandler(LevelGameLogic.FinishGameReason reason)
        {
            StartCoroutine(ShowGameOverWindowRoutine(ShowGameOverWindowDelaySec, _level.BestScore));

            var locKey = reason == LevelGameLogic.FinishGameReason.EnemyFire ? EnemyFireLockKey : CivilianDeathLockKey;
            _screen.ShowInfo(locKey);

            _level.TryUpdateBestScore(_gameLogic.Score);
        }

        private static void CloseGameOverWindowHandler(GameOverWindow.CloseResult result)
        {
            var scene = result == GameOverWindow.CloseResult.Restart ? GameScene.Level : GameScene.MainMenu;
            Game.LoadScene(scene);
        }

        private IEnumerator ShowGameOverWindowRoutine(float delaySec, int bestScore)
        {
            yield return new WaitForSeconds(delaySec);

            _screen.HideInfo();

            var data = new GameOverWindow.Data
            {
                Score = _gameLogic.Score,
                BestScore = bestScore,
                LocalizationManager = Game.Instance.LocalizationManager,
                CloseCallback = CloseGameOverWindowHandler
            };
            Game.Instance.UIManager.ShowWindow<GameOverWindow, GameOverWindow.Data>(in data);
        }
    }
}