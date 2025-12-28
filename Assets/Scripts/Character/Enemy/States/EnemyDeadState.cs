namespace Character.Enemy.States
{
    public class EnemyDeadState : BaseEnemyState
    {
        public EnemyDeadState(EnemyCharacter enemy) : base(enemy) { }

        public override void Enter()
        {
            Enemy.AnimationController.PlayDeath();
            Enemy.MovementController.Stop();
            Enemy.AnimationController.OnDeadEventTriggered += OnDeadEvent;
        }

        private void OnDeadEvent()
        {
            Enemy.AnimationController.OnDeadEventTriggered -= OnDeadEvent;
            Enemy.HealthController.OnDeadCompleted();
        }
    }
}
