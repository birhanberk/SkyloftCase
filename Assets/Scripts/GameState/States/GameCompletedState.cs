using Managers;
using UI;
using VContainer;

namespace GameState.States
{
    public class GameCompletedState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;
        
        public void Enter()
        {
            _uiManager.Show(UIPanelType.GameCompleted);
            _uiManager.Joystick.OnReset();
        }

        public void Exit() { }

        public void Tick() { }
    }
}
