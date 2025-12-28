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
        
        public PlayerMovementController MovementController => playerMovementController;
        public PlayerStateController StateController => playerStateController;
        public PlayerTargetController TargetController => playerTargetController;
        public PlayerAnimationController AnimationController => playerAnimationController;
        public PlayerWeaponController WeaponController => playerWeaponController;

        [Inject]
        private void Construct(IObjectResolver objectResolver)
        {
            objectResolver.Inject(playerMovementController);
            objectResolver.Inject(playerWeaponController);
            objectResolver.Inject(playerStateController);
            playerMovementController.OnStart(this);
            playerAnimationController.OnStart(this);
            playerStateController.OnStart(this);
            playerWeaponController.OnStart(this);
        }

        public void OnLevelStart()
        {
            HealthController.InitialSet();
            playerStateController.OnLevelStart();
            playerAnimationController.OnLevelStart();
            playerMovementController.OnLevelStart();
            playerTargetController.OnLevelStart();
        }
        
        protected void Update()
        {
            playerStateController.OnUpdate();
            playerMovementController.OnUpdate();
        }
    }
}
