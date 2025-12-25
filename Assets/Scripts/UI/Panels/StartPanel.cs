using GameState;
using GameState.States;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Panels
{
    public class StartPanel : BasePanel
    {
        [Header("Buttons")]
        [SerializeField] private Button startButton;

        [Inject] private GameStateMachine _gameStateMachine;
        
        private void OnEnable()
        {
            startButton.onClick.AddListener(StartButtonPerformed);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(StartButtonPerformed);
        }
        
        private void StartButtonPerformed()
        {
            _gameStateMachine.ChangeState<LevelTransitionState>();
        }
    }
}