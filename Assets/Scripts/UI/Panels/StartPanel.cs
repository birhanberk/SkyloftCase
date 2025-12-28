using GameState;
using GameState.States;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Panels
{
    public class StartPanel : BasePanel
    {
        [Header("Buttons")]
        [SerializeField] private Button startButton;

        [Header("Texts")]
        [SerializeField] private TMP_Text totalKilledText;

        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private PersistentDataManager _persistentDataManager;
        
        private void OnEnable()
        {
            startButton.onClick.AddListener(StartButtonPerformed);
            UpdateTotalKilledText();
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(StartButtonPerformed);
        }
        
        private void StartButtonPerformed()
        {
            _gameStateMachine.ChangeState<LevelTransitionState>();
        }

        private void UpdateTotalKilledText()
        {
            totalKilledText.text = _persistentDataManager.Load(_persistentDataManager.TotalKilledKey).ToString();
        }
    }
}