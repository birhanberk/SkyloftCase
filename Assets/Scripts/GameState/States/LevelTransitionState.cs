using Managers;
using UI;
using UI.Panels;
using VContainer;

namespace GameState.States
{
    public class LevelTransitionState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly GameStateMachine _stateMachine;

        public void Enter()
        {
            _uiManager.FadeIn(() =>
            {
                _levelManager.LoadNextLevel();
                _uiManager.Show(UIPanelType.Gameplay);
                _uiManager.FadeOut(() =>
                {
                    _stateMachine.ChangeState<PlayingState>();
                });
            });
        }

        public void Tick() { }

        public void Exit() { }
    }
}