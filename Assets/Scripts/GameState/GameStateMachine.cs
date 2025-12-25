using VContainer;
using VContainer.Unity;

namespace GameState
{
    public class GameStateMachine : ITickable
    {
        private IGameState _currentState;
        [Inject] private readonly IObjectResolver _resolver;

        public void ChangeState<T>() where T : IGameState
        {
            _currentState?.Exit();
            _currentState = _resolver.Resolve<T>();
            _currentState.Enter();
        }

        public void Tick()
        {
            _currentState?.Tick();
        }
    }
}