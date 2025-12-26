namespace Character.Common.States
{
    public interface ICharacterState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}
