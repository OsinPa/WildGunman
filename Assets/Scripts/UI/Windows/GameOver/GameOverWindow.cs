using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WildGunman.Localization;

namespace WildGunman.UI.Windows.GameOver
{
    public class GameOverWindow : UIDataWindow<GameOverWindow.Data>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _bestScoreText;
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _exitText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private const string BestScoreLocKey = "best_score";
        private const string ScoreLocKey = "score";
        private const string RestartLocKey = "restart";
        private const string ExitLocKey = "exit";
        private ILocalizationManager _localizationManager;
        private Action<CloseResult> _closeCallback;

        public override void Init(in Data data)
        {
            _localizationManager = data.LocalizationManager;
            _closeCallback = data.CloseCallback;

            _scoreText.text = _localizationManager.Get(ScoreLocKey, data.Score);
            _bestScoreText.text = _localizationManager.Get(BestScoreLocKey, data.BestScore);
            _restartText.text = _localizationManager.Get(RestartLocKey);
            _exitText.text = _localizationManager.Get(ExitLocKey);
        }

        protected override void OnShow()
        {
            base.OnShow();

            _restartButton.onClick.AddListener(RestartButtonHandler);
            _exitButton.onClick.AddListener(ExitButtonHandler);
        }

        protected override void OnHide()
        {
            _localizationManager = null;
            _closeCallback = null;

            _restartButton.onClick.RemoveListener(RestartButtonHandler);
            _exitButton.onClick.RemoveListener(ExitButtonHandler);

            base.OnHide();
        }

        private void RestartButtonHandler()
        {
            _closeCallback?.Invoke(CloseResult.Restart);
        }

        private void ExitButtonHandler()
        {
            _closeCallback?.Invoke(CloseResult.Exit);
        }

        public enum CloseResult
        {
            Restart,
            Exit
        }

        public struct Data
        {
            public int Score;
            public int BestScore;
            public ILocalizationManager LocalizationManager;
            public Action<CloseResult> CloseCallback;
        }
    }
}