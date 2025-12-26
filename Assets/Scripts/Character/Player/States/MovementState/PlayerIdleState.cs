using UnityEngine;

namespace Character.Player.States.MovementState
{
    public class PlayerIdleState : BasePlayerState
    {
        public PlayerIdleState(PlayerCharacter player) : base(player) { }

        public override void Enter()
        {
            Player.MovementController.SetMovement(Vector2.zero);
            Player.AnimationController.UpdateLocomotion(Vector2.zero, 0f);
        }

        public override void Tick()
        {
            Player.MovementController.SetMovement(Vector2.zero);
            Player.AnimationController.UpdateLocomotion(Vector2.zero, 0f);

            if (Player.MovementController.HasInput)
            {
                Player.StateController.ChangeMovementState(Player.MovementController.ShouldRun ? PlayerStateType.Run : PlayerStateType.Walk);
            }
        }
    }
}
