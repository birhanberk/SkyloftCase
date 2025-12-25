using Managers;
using UI;
using UI.Panels;
using VContainer;

namespace GameState.States
{
    public class LevelFailState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;

        public void Enter()
        {
            _uiManager.Show(UIPanelType.LevelFail);
        }

        public void Tick() { }

        public void Exit()
        {
        }
    }
}