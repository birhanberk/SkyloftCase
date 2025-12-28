namespace Character.Enemy.States
{
    public class EnemyAttackState : BaseEnemyState
    {
        public EnemyAttackState(EnemyCharacter enemy) : base(enemy) { }

        public override void Enter()
        {
            Enemy.AnimationController.PlayAttack();
            Enemy.AnimationController.OnAttackEventTriggered += OnAttack;
            Enemy.MovementController.Stop();
        }
        
        public override void Tick()
        {
            if (!Enemy.TargetController.Target.HealthController.IsAlive)
            {
                Enemy.StateController.ChangeState(EnemyStateType.Idle);
                return;
            }
            
            if (!Enemy.TargetController.IsInAttackRange())
            {
                Enemy.StateController.ChangeState(EnemyStateType.Walk);
            }
        }
        
        public override void Exit()
        {
            Enemy.AnimationController.OnAttackEventTriggered -= OnAttack;
        }

        private void OnAttack()
        {
            Enemy.TargetController.Attack();
        }
    }
}
