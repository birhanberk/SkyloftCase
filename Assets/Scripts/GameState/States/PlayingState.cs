using Managers;
using VContainer;

namespace GameState.States
{
    public class PlayingState : IGameState
    {
        [Inject] private readonly GameStateMachine _stateMachine;
        [Inject] private readonly LevelManager _levelManager;

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Tick()
        {
        }
        
        private void OnTimeFinished()
        {
            if (_levelManager.HasNextLevel())
            {
                _stateMachine.ChangeState<LevelSuccessState>();
            }
            else
            {
                _stateMachine.ChangeState<GameCompletedState>();
            }
        }
    }
}