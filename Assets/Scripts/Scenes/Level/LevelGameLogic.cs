using System;
using WildGunman.Controllers.Level;
using WildGunman.Timers;
using Random = UnityEngine.Random;

namespace WildGunman.Scenes.Level
{
    public class LevelGameLogic
    {
        public event Action<FinishGameReason> FinishGameAction;
        public event Action<int> ScoreChangedAction;
        public int Score { get; private set; }

        private readonly Timer _spawnTimer = new ();
        private readonly LevelCharactersConfig _config;
        private readonly LevelContent _levelContent;
        private int _spawnedCharactersCount;

        public LevelGameLogic(LevelCharactersConfig config, LevelContent levelContent)
        {
            _config = config;
            _levelContent = levelContent;

            foreach (var character in _levelContent.Characters)
            {
                character.SetActive(false);
                character.ClickAction += CharacterClickHandler;
                character.EndWaitingAction += CharacterEndWaitingHandler;
            }

            TryShuffleCharacters();
            StartSpawnTimer();
        }

        public void Update(float deltaTime)
        {
            _spawnTimer.Update(deltaTime);
        }

        private void FinishGame(FinishGameReason reason)
        {
            _spawnTimer.Stop();

            foreach (var character in _levelContent.Characters)
            {
                character.StopWaiting();
                character.ClickAction -= CharacterClickHandler;
                character.EndWaitingAction -= CharacterEndWaitingHandler;
            }

            FinishGameAction?.Invoke(reason);
        }

        private void HideCharacter(LevelCharacter character)
        {
            character.SetActive(false);
            --_spawnedCharactersCount;

            TryShuffleCharacters();
        }

        private void StartSpawnTimer()
        {
            var durationSec = Random.Range(_config.MinSpawnDelaySec, _config.MaxSpawnDelaySec);
            _spawnTimer.Start(durationSec, SpawnTimerHandler);
        }

        private void SpawnCharacters()
        {
            var charactersCount = Random.Range(1, _config.MaxSpawnCount + 1);
            foreach (var character in _levelContent.Characters)
            {
                if (character.IsActive)
                {
                    continue;
                }
                if (charactersCount == 0)
                {
                    break;
                }

                var delaySec = Random.Range(_config.MinWaitingDelaySec, _config.MaxWaitingDelaySec);
                character.SetActive(true);
                character.StartWaiting(delaySec);

                ++_spawnedCharactersCount;
                --charactersCount;
            }
        }

        private void TryShuffleCharacters()
        {
            if (_spawnedCharactersCount == 0)
            {
                _levelContent.ShuffleCharacters();
            }
        }

        private void SpawnTimerHandler()
        {
            SpawnCharacters();
            StartSpawnTimer();
        }

        private void CharacterClickHandler(LevelCharacter character)
        {
            if (!character.IsEnemy)
            {
                character.Highlight();
                FinishGame(FinishGameReason.CivilianDeath);
                return;
            }

            HideCharacter(character);

            Score += _config.BonusPoints;
            ScoreChangedAction?.Invoke(Score);
        }

        private void CharacterEndWaitingHandler(LevelCharacter character)
        {
            if (character.IsEnemy)
            {
                character.Highlight();
                FinishGame(FinishGameReason.EnemyFire);
                return;
            }

            HideCharacter(character);
        }

        public enum FinishGameReason
        {
            EnemyFire,
            CivilianDeath
        }
    }
}