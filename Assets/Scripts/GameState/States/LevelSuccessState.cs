using Managers;
using VContainer;

namespace GameState.States
{
    public class LevelSuccessState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;

        public void Enter()
        {
            _uiManager.ShowLevelSuccess(1);
        }

        public void Tick() { }

        public void Exit()
        {
        }
    }
}