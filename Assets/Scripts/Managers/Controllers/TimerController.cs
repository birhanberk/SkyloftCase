using System;
using System.Collections;
using UnityEngine;

namespace Managers.Controllers
{
    [Serializable]
    public class TimerController
    {
        private Coroutine _timerCoroutine;
        private MonoBehaviour _runner;
        private int _startSeconds;

        public int StartSeconds => _startSeconds;
        
        public Action<int> OnTick;
        public Action OnCompleted;

        public void StartTimer(MonoBehaviour runner, int seconds)
        {
            StopTimer();
            _startSeconds = seconds;
            _runner = runner;
            _timerCoroutine = _runner.StartCoroutine(TimerRoutine(seconds));
        }

        public void StopTimer()
        {
            if (_timerCoroutine != null && _runner)
            {
                _runner.StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
            }
        }

        private IEnumerator TimerRoutine(int seconds)
        {
            var remaining = seconds;

            while (remaining > 0)
            {
                OnTick?.Invoke(remaining);
                yield return new WaitForSeconds(1f);
                remaining--;
            }

            OnTick?.Invoke(0);
            OnCompleted?.Invoke();
            _timerCoroutine = null;
        }
    }
}