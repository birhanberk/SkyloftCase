using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Panels
{
    public class FadePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }

        public void FadeIn(Action onComplete)
        {
            canvasGroup.alpha = 0;

            canvasGroup
                .DOFade(1f, 0.5f)
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        public void FadeOut(Action onComplete)
        {
            canvasGroup.alpha = 1;

            canvasGroup
                .DOFade(0f, 0.5f)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }
    }
}