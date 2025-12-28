namespace Character.Player.States.CombatState
{
    public class PlayerSearchState : BasePlayerState
    {
        public PlayerSearchState(PlayerCharacter player) : base(player) { }
        
        public override void Enter()
        {
            Player.AnimationController.PlaySearch();
        }
        
        public override void Tick()
        {
            if (Player.TargetController.HasTarget && Player.WeaponController.CanAttack)
            {
                Player.StateController.ChangeCombatState(PlayerStateType.Attack);
            }
        }
    }
}
