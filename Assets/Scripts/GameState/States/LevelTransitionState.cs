using Managers;
using Pool;
using UI;
using VContainer;

namespace GameState.States
{
    public class LevelTransitionState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly GameStateMachine _stateMachine;
        [Inject] private readonly GameManager _gameManager;
        [Inject] private readonly PoolManager _poolManager;

        public void Enter()
        {
            _uiManager.FadeIn(() =>
            {
                SetupGame();
                _uiManager.FadeOut(() =>
                {
                    _stateMachine.ChangeState<PlayingState>();
                });
            });
        }

        private void SetupGame()
        {
            _levelManager.LoadLevel();
            _gameManager.CameraController.Setup(_gameManager.PlayerController.PlayerCharacter.transform);
            _uiManager.Show(UIPanelType.Gameplay);
        }

        public void Tick() { }

        public void Exit() { }
    }
}