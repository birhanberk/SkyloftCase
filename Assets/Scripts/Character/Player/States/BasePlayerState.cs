using Character.Common.States;

namespace Character.Player.States
{
    public abstract class BasePlayerState : ICharacterState
    {
        protected readonly PlayerCharacter Player;

        protected BasePlayerState(PlayerCharacter player)
        {
            Player = player;
        }

        public virtual void Enter() { }
        public virtual void Tick() { }
        public virtual void Exit() { }
    }
}
