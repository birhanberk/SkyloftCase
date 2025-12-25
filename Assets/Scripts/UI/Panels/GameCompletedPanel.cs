using GameState;
using GameState.States;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Panels
{
    public class GameCompletedPanel : BasePanel
    {
        [Header("Buttons")]
        [SerializeField] private Button returnButton;
        
        [Inject] private GameStateMachine _gameStateMachine;
        
        private void OnEnable()
        {
            returnButton.onClick.AddListener(ReturnButtonPerformed);
        }

        private void OnDisable()
        {
            returnButton.onClick.RemoveListener(ReturnButtonPerformed);
        }

        private void ReturnButtonPerformed()
        {
            _gameStateMachine.ChangeState<LevelTransitionState>();
        }
    }
}
