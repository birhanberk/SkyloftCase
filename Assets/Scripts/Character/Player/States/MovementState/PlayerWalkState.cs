namespace Character.Player.States.MovementState
{
    public class PlayerWalkState : BasePlayerState
    {
        public PlayerWalkState(PlayerCharacter player) : base(player) { }

        public override void Enter()
        {
            Player.AnimationController.UpdateLocomotion(Player.MovementController.GetLocalMoveInput(), Player.MovementController.Speed);
        }
        
        public override void Tick()
        {
            Player.MovementController.ApplyInput();
            Player.AnimationController.UpdateLocomotion(Player.MovementController.GetLocalMoveInput(), Player.MovementController.Speed);

            if (!Player.MovementController.HasInput)
            {
                Player.StateController.ChangeMovementState(PlayerStateType.Idle);
                return;
            }

            if (Player.MovementController.ShouldRun)
            {
                Player.StateController.ChangeMovementState(PlayerStateType.Run);
            }
        }
    }
}

