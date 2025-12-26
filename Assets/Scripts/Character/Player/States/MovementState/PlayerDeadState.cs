namespace Character.Player.States.MovementState
{
    public class PlayerDeadState : BasePlayerState
    {
        private bool _entered;

        public PlayerDeadState(PlayerCharacter player) : base(player) { }

        public override void Enter()
        {
            if (_entered) return;
            _entered = true;

            Player.MovementController.StopImmediately();

            Player.TargetController.ClearTargets();

            Player.AnimationController.PlayDeath();
        }
    }
}
