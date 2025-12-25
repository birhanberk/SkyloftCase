using GameState;
using GameState.States;
using VContainer;
using VContainer.Unity;

namespace Managers
{
    public class GameManager : IStartable
    {
        [Inject] private GameStateMachine _stateMachine;

        public void Start()
        {
            _stateMachine.ChangeState<StartState>();
        }
    }
}
