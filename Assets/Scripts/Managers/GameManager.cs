using System;
using GameState;
using GameState.States;
using Managers.Controllers;
using UnityEngine;
using VContainer;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private TimerController timerController;
        [SerializeField] private VignetteController vignetteController;
        
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private GameStateMachine _gameStateMachine;

        public PlayerController PlayerController => playerController;
        public CameraController CameraController => cameraController;
        public TimerController TimerController => timerController;
        public VignetteController VignetteController => vignetteController;

        public Action OnGameStarted;
        public Action OnGameEnded;

        private void Awake()
        {
            vignetteController.OnAwake();
        }

        public void Start()
        {
            _objectResolver.Inject(playerController);
            _gameStateMachine.ChangeState<StartState>();
            playerController.CreatePlayer();
        }

        private void LateUpdate()
        {
            cameraController.OnLateUpdate();
        }

        public void OnLevelStart()
        {
            playerController.OnLevelStart();
            OnGameStarted?.Invoke();
        }

        public void OnLevelEnd()
        {
            timerController.StopTimer();
            playerController.OnLevelEnd();
            cameraController.OnLevelEnd();
            OnGameEnded?.Invoke();
        }
    }
}
