using Character.Common;
using Character.Player.Controllers;
using UnityEngine;
using VContainer;

namespace Character.Player
{
    public class PlayerCharacter : BaseCharacter
    {
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerStateController playerStateController;
        [SerializeField] private PlayerTargetController playerTargetController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerWeaponController playerWeaponController;
        
        [Inject] private IObjectResolver _objectResolver;

        public PlayerMovementController MovementController => playerMovementController;
        public PlayerStateController StateController => playerStateController;
        public PlayerTargetController TargetController => playerTargetController;
        public PlayerAnimationController AnimationController => playerAnimationController;

        protected override void Start()
        {
            base.Start();
            _objectResolver.Inject(playerMovementController);
            playerMovementController.OnStart(this);
            playerAnimationController.OnStart(this);
            playerStateController.OnStart(this);
            playerWeaponController.OnStart(this);
        }
        
        protected void Update()
        {
            playerStateController.OnUpdate();
            playerMovementController.OnUpdate();
        }
    }
}
