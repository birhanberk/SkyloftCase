using GameState;
using GameState.States;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Panels
{
    public class GameCompletedPanel : BasePanel
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text totalKilledText;
        
        [Header("Buttons")]
        [SerializeField] private Button returnButton;
        
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private PersistentDataManager _persistentDataManager;

        private void OnEnable()
        {
            returnButton.onClick.AddListener(ReturnButtonPerformed);
            UpdateTotalKilledText();
        }

        private void OnDisable()
        {
            returnButton.onClick.RemoveListener(ReturnButtonPerformed);
        }

        private void ReturnButtonPerformed()
        {
            _gameStateMachine.ChangeState<StartState>();
        }
        
        private void UpdateTotalKilledText()
        {
            totalKilledText.text = _persistentDataManager.Load(_persistentDataManager.TotalKilledKey).ToString();
        }
    }
}
