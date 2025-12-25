using Managers;
using UI;
using UI.Panels;
using VContainer;

namespace GameState.States
{
    public class StartState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;

        public void Enter()
        {
            _uiManager.Show(UIPanelType.Start);
        }

        public void Tick() { }

        public void Exit() { }
    }
}