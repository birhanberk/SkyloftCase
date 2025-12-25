using UnityEngine;

namespace UI
{
    public abstract class BasePanel : MonoBehaviour
    {
        public void Show()
        {
            SetVisible(true);
        }

        public void Hide()
        {
            SetVisible(false);
        }

        private void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}
