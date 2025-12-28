using Managers;
using UI;
using VContainer;

namespace GameState.States
{
    public class LevelFailState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;
        [Inject] private readonly GameManager _gameManager;
        [Inject] private readonly LevelManager _levelManager;

        public void Enter()
        {
            _uiManager.Show(UIPanelType.LevelFail);
            _levelManager.CurrentLevel.OnCompleted();
            _uiManager.Joystick.OnReset();
        }

        public void Tick() { }

        public void Exit()
        {
        }
    }
}