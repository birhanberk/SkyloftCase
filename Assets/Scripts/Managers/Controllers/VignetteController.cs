using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Managers.Controllers
{
    [Serializable]
    public class VignetteController
    {
        [SerializeField] private Volume volume;
        [SerializeField] private Color damageColor = new (1f, 0f, 0f, 1f);
        [SerializeField, Range(0f, 1f)] private float peakIntensity = 0.55f;
        [SerializeField, Range(0f, 1f)] private float smoothness = 0.9f;
        [SerializeField] private float upTime = 0.08f;
        [SerializeField] private float downTime = 0.25f;

        private Vignette _vignette;
        private Coroutine _vignetteCoroutine;

        public void OnAwake()
        {
            if (volume.profile.TryGet(out _vignette))
            {
                _vignette.color.value = damageColor;
                _vignette.intensity.value = 0f;
                _vignette.smoothness.value = smoothness;
            }
        }

        public void Flash()
        {
            if (_vignetteCoroutine != null)
            {
                volume.StopCoroutine(_vignetteCoroutine);
            }
            _vignetteCoroutine = volume.StartCoroutine(FlashCoroutine());
        }

        private IEnumerator FlashCoroutine()
        {
            var time = 0f;
            while (time < upTime)
            {
                time += Time.deltaTime;
                _vignette.intensity.value = Mathf.Lerp(0f, peakIntensity, time / upTime);
                yield return null;
            }

            time = 0f;
            while (time < downTime)
            {
                time += Time.deltaTime;
                _vignette.intensity.value = Mathf.Lerp(peakIntensity, 0f, time / downTime);
                yield return null;
            }

            _vignette.intensity.value = 0f;
            _vignetteCoroutine = null;
        }
    }
}
