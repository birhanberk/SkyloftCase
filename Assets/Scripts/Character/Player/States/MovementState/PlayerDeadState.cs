namespace Character.Player.States.MovementState
{
    public class PlayerDeadState : BasePlayerState
    {
        public PlayerDeadState(PlayerCharacter player) : base(player) { }

        public override void Enter()
        {
            Player.MovementController.StopMovement();

            Player.TargetController.ClearTargets();
            Player.TargetController.SetDetection(false);

            Player.AnimationController.PlayDeath();
            
            Player.StateController.ChangeCombatState(PlayerStateType.Search);

            Player.AnimationController.OnDeadEventTriggered += OnDeadEvent;
        }

        private void OnDeadEvent()
        {
            Player.AnimationController.OnDeadEventTriggered -= OnDeadEvent;
            Player.HealthController.OnDeadCompleted();
        }
    }
}
