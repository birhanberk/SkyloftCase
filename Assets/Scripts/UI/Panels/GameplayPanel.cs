using Managers;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI.Panels
{
    public class GameplayPanel : BasePanel
    {
        [Inject] private GameManager _gameManager;
        [Inject] private LevelManager _levelManager;
        
        [Header("Texts")]
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text killCountText;

        private void OnEnable()
        {
            _gameManager.TimerController.OnTick += UpdateTimerText;
            _levelManager.CurrentLevel.OnEnemyKilled += UpdateKillText;

            UpdateLevelText(_levelManager.CurrentLevelIndex + 1);
            UpdateTimerText(_gameManager.TimerController.StartSeconds);
            UpdateKillText(0);
        }

        private void OnDisable()
        {
            _gameManager.TimerController.OnTick -= UpdateTimerText;
            _levelManager.CurrentLevel.OnEnemyKilled -= UpdateKillText;
        }

        private void UpdateLevelText(int level)
        {
            levelText.text = level.ToString();
        }

        private void UpdateTimerText(int seconds)
        {
            timerText.text = seconds.ToString();
        }

        private void UpdateKillText(int killCount)
        {
            killCountText.text = killCount.ToString();
        }
    }
}