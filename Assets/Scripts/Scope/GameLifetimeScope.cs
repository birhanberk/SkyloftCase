using GameState;
using GameState.States;
using Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scope
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private LevelManager levelManager;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // Core systems
            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.Register<GameManager>(Lifetime.Singleton);
            builder.RegisterComponent(uiManager);
            builder.RegisterComponent(levelManager);
            
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
