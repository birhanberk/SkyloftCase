using Managers;
using UI;
using VContainer;

namespace GameState.States
{
    public class StartState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;
        [Inject] private readonly LevelManager _levelManager;

        public void Enter()
        {
            _uiManager.Show(UIPanelType.Start);
            _levelManager.DestroyLevel();
            _uiManager.Joystick.OnReset();
        }

        public void Tick() { }

        public void Exit() { }
    }
}