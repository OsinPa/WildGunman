using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WildGunman.Controllers.Level;
using WildGunman.Localization;

namespace WildGunman.UI.Screens.MainMenu
{
    public class MainMenuScreen : UIDataScreen<MainMenuScreen.Data>
    {
        [SerializeField] private TMP_Dropdown _levelDropdown;
        [SerializeField] private TextMeshProUGUI _selectLevelText;
        [SerializeField] private TextMeshProUGUI _playText;
        [SerializeField] private TextMeshProUGUI _bestScoreText;
        [SerializeField] private Button _playButton;

        private const string SelectLevelLocKey = "select_level";
        private const string BestScoreLocKey = "best_score";
        private const string PlayLocKey = "play";
        private ILocalizationManager _localizationManager;
        private Action<LevelModel> _playLevelCallback;

        public override void Init(in Data data)
        {
            _localizationManager = data.LocalizationManager;
            _playLevelCallback = data.PlayLevelCallback;
            
            _selectLevelText.text = _localizationManager.Get(SelectLevelLocKey);
            _playText.text = _localizationManager.Get(PlayLocKey);

            _levelDropdown.options.Clear();
            foreach (var level in data.Levels)
            {
                var optionName = _localizationManager.Get(level.Config.LocKey);
                _levelDropdown.options.Add(new LevelOptionData(optionName, level));
            }
            _levelDropdown.options.Sort();
        }

        protected override void OnShow()
        {
            base.OnShow();

            UpdateScore(_levelDropdown.value);

            _levelDropdown.onValueChanged.AddListener(DropdownValueChangedHandler);
            _playButton.onClick.AddListener(PlayButtonHandler);
        }

        protected override void OnHide()
        {
            _localizationManager = null;
            _playLevelCallback = null;

            _levelDropdown.onValueChanged.RemoveListener(DropdownValueChangedHandler);
            _playButton.onClick.RemoveListener(PlayButtonHandler);

            base.OnHide();
        }

        private void UpdateScore(int levelIndex)
        {
            if (_levelDropdown.options[levelIndex] is LevelOptionData levelOptionData)
            {
                _bestScoreText.text = _localizationManager.Get(BestScoreLocKey, levelOptionData.Level.BestScore);
            }
        }

        private void DropdownValueChangedHandler(int index)
        {
            UpdateScore(index);
        }

        private void PlayButtonHandler()
        {
            if (_levelDropdown.options[_levelDropdown.value] is LevelOptionData levelOptionData)
            {
                _playLevelCallback?.Invoke(levelOptionData.Level);
            }
        }

        public struct Data
        {
            public IEnumerable<LevelModel> Levels;
            public ILocalizationManager LocalizationManager;
            public Action<LevelModel> PlayLevelCallback;
        }
    }
}