namespace Character.Player.States.CombatState
{
    public class PlayerAttackState : BasePlayerState
    {
        public PlayerAttackState(PlayerCharacter player) : base(player) { }
        
        public override void Enter()
        {
            Player.AnimationController.PlayAttack();
            Player.AnimationController.EnableLookAt(Player.TargetController.GetClosestTarget().TargetTransform);
        }
        
        public override void Tick()
        {
            if (!Player.TargetController.HasTarget)
            {
                Player.StateController.ChangeCombatState(PlayerStateType.Search);
            }
        }
    }
}
