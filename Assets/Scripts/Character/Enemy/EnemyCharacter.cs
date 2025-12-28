using Character.Common;
using Character.Enemy.Controllers;
using Pool;
using UnityEngine;
using VContainer;

namespace Character.Enemy
{
    public class EnemyCharacter : BaseCharacter, ITargetable, IPoolable
    {
        [SerializeField] private EnemyStateController stateController;
        [SerializeField] private EnemyAnimationController animationController;
        [SerializeField] private EnemyTargetController targetController;
        [SerializeField] private EnemyMovementController movementController;
        
        private IObjectResolver _objectResolver;
        private PoolManager _poolManager;
        
        public Transform HitTransform => targetController.HitTransform;
        public Transform TargetTransform => transform;
        public bool IsAlive => HealthController.IsAlive;
        public EnemyStateController StateController => stateController;
        public EnemyAnimationController AnimationController => animationController;
        public EnemyTargetController TargetController => targetController;
        public EnemyMovementController MovementController => movementController;

        [Inject]
        private void Construct(IObjectResolver objectResolver, PoolManager poolManager)
        {
            _objectResolver = objectResolver;
            _poolManager = poolManager;
            _objectResolver.Inject(targetController);
            stateController.OnStart(this);
            targetController.OnStart(this);
            movementController.OnStart(this);
        }

        private void Update()
        {
            stateController.OnUpdate();
        }

        public void OnExitPool()
        {
            HealthController.InitialSet();
            HealthController.OnDeadCompleteAction += OnDeadCompleted;
            stateController.OnExitPool();
            movementController.OnExitPool();
        }

        public void OnEnterPool()
        {
            movementController.OnEnterPool();
        }

        public void TakeDamage(float damage)
        {
            HealthController.TakeDamage(damage);
        }

        private void OnDeadCompleted()
        {
            HealthController.OnDeadCompleteAction -= OnDeadCompleted;
            _poolManager.Release((EnemyData)baseCharacterData, this);
        }
    }
}
