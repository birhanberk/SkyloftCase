namespace Character.Common.States
{
    public class CharacterStateMachine
    {
        private ICharacterState _currentState;

        public void ChangeState(ICharacterState newState)
        {
            if (_currentState == newState)
                return;

            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Tick()
        {
            _currentState?.Tick();
        }
    }
}
