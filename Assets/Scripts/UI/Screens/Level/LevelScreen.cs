using TMPro;
using UnityEngine;
using WildGunman.Localization;

namespace WildGunman.UI.Screens.Level
{
    public class LevelScreen : UIDataScreen<LevelScreen.Data>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private TextMeshProUGUI _hintText;

        private const string ScoreLocKey = "score";
        private const string HintLocKey = "level_hint";
        private ILocalizationManager _localizationManager;

        public override void Init(in Data data)
        {
            _localizationManager = data.LocalizationManager;
            _hintText.text = _localizationManager.Get(HintLocKey);
        }

        protected override void OnShow()
        {
            base.OnShow();

            UpdateScore(0);
            HideInfo();
        }

        protected override void OnHide()
        {
            _localizationManager = null;

            base.OnHide();
        }

        public void UpdateScore(int score)
        {
            _scoreText.text = _localizationManager.Get(ScoreLocKey, score);
        }

        public void ShowInfo(string locKey)
        {
            _infoText.text = _localizationManager.Get(locKey);
            _infoText.gameObject.SetActive(true);
        }

        public void HideInfo()
        {
            _infoText.gameObject.SetActive(false);
        }

        public struct Data
        {
            public ILocalizationManager LocalizationManager;
        }
    }
}