using Character.Common.States;

namespace Character.Enemy.States
{
    public class BaseEnemyState : ICharacterState
    {
        protected readonly EnemyCharacter Enemy;

        protected BaseEnemyState(EnemyCharacter enemy)
        {
            Enemy = enemy;
        }

        public virtual void Enter() { }
        public virtual void Tick() { }
        public virtual void Exit() { }
    }
}
