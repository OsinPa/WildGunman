using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using WildGunman.Scenes.Level;

namespace WildGunman.Editor
{
    [CustomEditor(typeof(LevelContent))]
    public class LevelContentEditor : UnityEditor.Editor
    {
        private SerializedProperty _charactersProperty;

        private void OnEnable()
        {
            _charactersProperty = serializedObject.FindProperty("_characters");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update Fields"))
            {
                var characters = FindCharacters();
                _charactersProperty.arraySize = characters.Length;
                for (var i = 0; i < characters.Length; i++)
                {
                    _charactersProperty.GetArrayElementAtIndex(i).objectReferenceValue = characters[i];
                }

                serializedObject.ApplyModifiedProperties();
            }
        }

        private static LevelCharacter[] FindCharacters()
        {
            var currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (currentPrefabStage != null)
            {
                return currentPrefabStage.FindComponentsOfType<LevelCharacter>();
            }

            if (Selection.activeGameObject != null)
            {
                return Selection.activeGameObject.GetComponentsInChildren<LevelCharacter>();
            }
            
            return FindObjectsOfType<LevelCharacter>();
        }
    }
}