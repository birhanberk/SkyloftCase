using Managers;
using UI;
using UI.Panels;
using VContainer;

namespace GameState.States
{
    public class GameCompletedState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;
        
        public void Enter()
        {
            _uiManager.Show(UIPanelType.GameCompleted);
        }

        public void Exit() { }

        public void Tick() { }
    }
}
