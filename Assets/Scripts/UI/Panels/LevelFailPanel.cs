using GameState;
using GameState.States;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Panels
{
    public class LevelFailPanel : BasePanel
    {
        [Header("Buttons")]
        [SerializeField] private Button returnButton;
        [SerializeField] private Button restartButton;

        [Inject] private GameStateMachine _gameStateMachine;
        
        private void OnEnable()
        {
            returnButton.onClick.AddListener(ReturnButtonPerformed);
            restartButton.onClick.AddListener(RestartButtonPerformed);
        }

        private void OnDisable()
        {
            returnButton.onClick.RemoveListener(ReturnButtonPerformed);
            restartButton.onClick.RemoveListener(RestartButtonPerformed);
        }
        
        private void ReturnButtonPerformed()
        {
            _gameStateMachine.ChangeState<StartState>();
        }
        
        private void RestartButtonPerformed()
        {
            _gameStateMachine.ChangeState<LevelTransitionState>();
        }
    }
}