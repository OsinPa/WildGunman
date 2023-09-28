using UnityEngine;

namespace WildGunman.UI.Screens
{
    public abstract class UIScreen : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            OnHide();
            gameObject.SetActive(false);
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}