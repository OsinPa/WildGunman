using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WildGunman.Scenes.Level
{
    public class LevelCharacter : MonoBehaviour, IPointerClickHandler
    {
        public event Action<LevelCharacter> ClickAction;
        public event Action<LevelCharacter> EndWaitingAction;
        public bool IsEnemy => _isEnemy;
        public bool IsActive => gameObject.activeSelf;

        [SerializeField] private bool _isEnemy;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private readonly Color _highlightColor = Color.red;
        private Coroutine _waitingCoroutine;

        public void StartWaiting(float delaySec)
        {
            _waitingCoroutine = StartCoroutine(WaitRoutine(delaySec));
        }

        public void StopWaiting()
        {
            if (_waitingCoroutine != null)
            {
                StopCoroutine(_waitingCoroutine);
            }
        }

        public void Highlight()
        {
            _spriteRenderer.color = _highlightColor;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
            StopWaiting();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ClickAction?.Invoke(this);
        }

        private IEnumerator WaitRoutine(float delaySec)
        {
            yield return new WaitForSeconds(delaySec);
            EndWaitingAction?.Invoke(this);
        }
    }
}