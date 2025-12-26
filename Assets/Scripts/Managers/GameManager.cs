using Character.Player;
using GameState;
using GameState.States;
using UnityEngine;
using VContainer;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private GameStateMachine _gameStateMachine;

        public PlayerController PlayerController => playerController;

        public void Start()
        {
            _objectResolver.Inject(playerController);
            _gameStateMachine.ChangeState<StartState>();
        }

        public void OnGameStart()
        {
            _gameStateMachine.ChangeState<LevelTransitionState>();
            playerController.CreatePlayer();
        }

        public void OnGameEnd()
        {
            playerController.DestroyPlayer();
        }
    }
}
