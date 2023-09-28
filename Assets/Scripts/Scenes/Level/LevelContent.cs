using System.Collections.Generic;
using UnityEngine;
using WildGunman.Logger;

namespace WildGunman.Scenes.Level
{
    public class LevelContent : MonoBehaviour
    {
        public IEnumerable<LevelCharacter> Characters => _characters;

        [SerializeField] private LevelCharacter[] _characters;

        private static readonly ILog Log = LogManager.GetLogger(typeof(LevelContent));

        private void Awake()
        {
            foreach (var character in _characters)
            {
                if (character == null)
                {
                    _characters = FindObjectsOfType<LevelCharacter>();
                    Log.AddField("Name", name).Warn("The list of characters contained an empty value");

                    break;
                }
            }
        }

        public void ShuffleCharacters()
        {
            for (var i = _characters.Length - 1; i >= 0; i--)
            {
                var j = Random.Range(0, i + 1);
                (_characters[i], _characters[j]) = (_characters[j], _characters[i]);

                j = Random.Range(0, i + 1);
                (_characters[i].transform.position, _characters[j].transform.position) = (_characters[j].transform.position, _characters[i].transform.position);
            }
        }
    }
}