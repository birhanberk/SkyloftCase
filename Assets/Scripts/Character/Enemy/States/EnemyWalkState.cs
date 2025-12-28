namespace Character.Enemy.States
{
    public class EnemyWalkState : BaseEnemyState
    {
        public EnemyWalkState(EnemyCharacter enemy) : base(enemy) { }

        public override void Enter()
        {
            Enemy.AnimationController.PlayWalk();
            Enemy.MovementController.MoveToTarget();
        }

        public override void Tick()
        {
            if (!Enemy.TargetController.Target.HealthController.IsAlive)
            {
                Enemy.StateController.ChangeState(EnemyStateType.Idle);
                return;
            }
            
            if (Enemy.TargetController.IsInAttackRange())
            {
                Enemy.StateController.ChangeState(EnemyStateType.Attack);
                return;
            }

            Enemy.MovementController.MoveToTarget();
        }
    }
}
