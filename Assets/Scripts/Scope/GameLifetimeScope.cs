using GameState;
using GameState.States;
using Managers;
using Pool;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scope
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private PoolManager poolManager;
        [SerializeField] private PersistentDataManager persistentDataManager;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // Core systems
            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.RegisterComponent(gameManager);
            builder.RegisterComponent(uiManager);
            builder.RegisterComponent(levelManager);
            builder.RegisterComponent(poolManager);
            builder.RegisterComponent(persistentDataManager);
            
            // States
            builder.Register<StartState>(Lifetime.Transient);
            builder.Register<LevelTransitionState>(Lifetime.Transient);
            builder.Register<PlayingState>(Lifetime.Transient);
            builder.Register<LevelSuccessState>(Lifetime.Transient);
            builder.Register<LevelFailState>(Lifetime.Transient);
            builder.Register<GameCompletedState>(Lifetime.Transient);

            // Tick
            builder.RegisterEntryPoint<GameStateMachine>();
            builder.RegisterEntryPoint<GameManager>();
        }

        protected override void Awake()
        {
            base.Awake();
            
            Application.targetFrameRate = 60;
        }
    }
}
