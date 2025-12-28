namespace Character.Enemy.States
{
    public class EnemyIdleState : BaseEnemyState
    {
        public EnemyIdleState(EnemyCharacter enemy) : base(enemy) { }

        public override void Enter()
        {
            Enemy.AnimationController.PlayIdle();
            Enemy.MovementController.Stop();
        }

        public override void Tick()
        {
            if (Enemy.TargetController.Target.HealthController.IsAlive)
            {
                Enemy.StateController.ChangeState(!Enemy.TargetController.IsInAttackRange()
                    ? EnemyStateType.Walk
                    : EnemyStateType.Attack);
            }
        }
    }
}
