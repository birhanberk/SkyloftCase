namespace Character.Player.States.CombatState
{
    public class PlayerAttackState : BasePlayerState
    {
        public PlayerAttackState(PlayerCharacter player) : base(player) { }
        
        public override void Enter()
        {
            Player.AnimationController.PlayAttack();
            Player.AnimationController.SetIKTarget(Player.TargetController.GetClosestTarget().HitTransform);
            Player.AnimationController.OnAttackEventTriggered += OnAttack;
        }
        
        public override void Tick()
        {
            if (!Player.TargetController.HasTarget || !Player.WeaponController.CanAttack)
            {
                Player.StateController.ChangeCombatState(PlayerStateType.Search);
                return;
            }
            Player.AnimationController.SetIKTarget(Player.TargetController.GetClosestTarget().HitTransform);
        }

        public override void Exit()
        {
            Player.AnimationController.OnAttackEventTriggered -= OnAttack;
        }

        private void OnAttack()
        {
            Player.WeaponController.Attack();
        }
    }
}
