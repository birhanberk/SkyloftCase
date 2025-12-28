using System;
using Character.Player;
using GameState;
using GameState.States;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace Managers.Controllers
{
    [Serializable]
    public class PlayerController
    {
        [SerializeField] private Transform sceneParent;
        [SerializeField] private PlayerCharacter playerPrefab;

        [Inject] private IObjectResolver _objectResolver;
        [Inject] private GameStateMachine _gameStateMachine;
        
        public PlayerCharacter PlayerCharacter { get; private set; }
        
        public void CreatePlayer()
        {
            PlayerCharacter = Object.Instantiate(playerPrefab, sceneParent);
            _objectResolver.Inject(PlayerCharacter);
            PlayerCharacter.gameObject.SetActive(false);
        }

        public void OnLevelStart()
        {
            PlayerCharacter.gameObject.SetActive(true);
            PlayerCharacter.OnLevelStart();
            PlayerCharacter.HealthController.OnDeadCompleteAction += OnPlayerDead;
        }

        public void OnLevelEnd()
        {
            PlayerCharacter.gameObject.SetActive(false);
            PlayerCharacter.HealthController.OnDeadCompleteAction -= OnPlayerDead;
            PlayerCharacter.MovementController.StopMovement();
            PlayerCharacter.transform.position = Vector3.zero;
            PlayerCharacter.transform.eulerAngles = Vector3.zero;
        }

        private void OnPlayerDead()
        {
            PlayerCharacter.HealthController.OnDeadCompleteAction -= OnPlayerDead;
            _gameStateMachine.ChangeState<LevelFailState>();
        }
    }
}
