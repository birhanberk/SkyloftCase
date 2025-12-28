using Managers;
using VContainer;

namespace GameState.States
{
    public class PlayingState : IGameState
    {
        [Inject] private readonly GameStateMachine _stateMachine;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly GameManager _gameManager;

        public void Enter()
        {
            _gameManager.OnLevelStart();
            _levelManager.CurrentLevel.StartLevel();
            _gameManager.TimerController.StartTimer(_gameManager, _levelManager.CurrentLevel.LevelData.LevelTime);
            _gameManager.TimerController.OnCompleted += OnTimeFinished;
        }

        public void Exit()
        {
            _gameManager.OnLevelEnd();
            _levelManager.CurrentLevel.OnCompleted();
        }

        public void Tick()
        {
        }
        
        private void OnTimeFinished()
        {
            _gameManager.TimerController.OnCompleted -= OnTimeFinished;

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