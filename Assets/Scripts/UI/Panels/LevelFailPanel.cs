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
        [SerializeField] private Button restartButton;

        [Inject] private GameStateMachine _gameStateMachine;
        
        private void OnEnable()
        {
            restartButton.onClick.AddListener(RestartButtonPerformed);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(RestartButtonPerformed);
        }
        
        private void RestartButtonPerformed()
        {
            _gameStateMachine.ChangeState<LevelTransitionState>();
        }
    }
}