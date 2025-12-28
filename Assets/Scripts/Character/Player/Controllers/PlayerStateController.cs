using System;
using System.Collections.Generic;
using Character.Common;
using Character.Common.States;
using Character.Player.States;
using Character.Player.States.CombatState;
using Character.Player.States.MovementState;
using Managers;
using UnityEngine;
using VContainer;

namespace Character.Player.Controllers
{
    [Serializable]
    public class PlayerStateController
    {
        private PlayerCharacter _playerCharacter;

        [Inject] private GameManager _gameManager;
        
        private CharacterStateMachine _movementStateMachine;
        private CharacterStateMachine _combatStateMachine;
        
        private Dictionary<PlayerStateType, BasePlayerState> _movementStateMap = new ();
        private Dictionary<PlayerStateType, BasePlayerState> _combatStateMap = new ();
        
        public void OnStart(PlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;

            CreateStateMachines();
            CreateStates();
            
            ChangeMovementState(PlayerStateType.Idle);
            ChangeCombatState(PlayerStateType.Search);
        }

        public void OnLevelStart()
        {
            _playerCharacter.HealthController.OnHealthChanged += OnHealthChanged;
            _playerCharacter.HealthController.OnDeadAction += OnDead;
            ChangeMovementState(PlayerStateType.Idle);
            ChangeCombatState(PlayerStateType.Search);
        }

        public void OnUpdate()
        {
            _movementStateMachine.Tick();
            _combatStateMachine.Tick();
        }

        private void CreateStateMachines()
        {
            _movementStateMachine = new CharacterStateMachine();
            _combatStateMachine = new CharacterStateMachine();
        }

        private void CreateStates()
        {
            _movementStateMap.Add(PlayerStateType.Idle, new PlayerIdleState(_playerCharacter));
            _movementStateMap.Add(PlayerStateType.Walk, new PlayerWalkState(_playerCharacter));
            _movementStateMap.Add(PlayerStateType.Run, new PlayerRunState(_playerCharacter));
            _movementStateMap.Add(PlayerStateType.Dead, new PlayerDeadState(_playerCharacter));
            
            _combatStateMap.Add(PlayerStateType.Search, new PlayerSearchState(_playerCharacter));
            _combatStateMap.Add(PlayerStateType.Attack, new PlayerAttackState(_playerCharacter));
        }

        public void ChangeMovementState(PlayerStateType stateType)
        {
            if (_movementStateMap.TryGetValue(stateType, out var movementState))
            {
                _movementStateMachine.ChangeState(movementState);
            }
        }
        
        public void ChangeCombatState(PlayerStateType stateType)
        {
            if (_combatStateMap.TryGetValue(stateType, out var combatState))
            {
                _combatStateMachine.ChangeState(combatState);
            }
        }

        private void OnDead(BaseCharacter character)
        {
            _playerCharacter.HealthController.OnDeadAction -= OnDead;
            _playerCharacter.HealthController.OnHealthChanged -= OnHealthChanged;
            ChangeMovementState(PlayerStateType.Dead);
        }

        private void OnHealthChanged(float health, float maxHealth)
        {
            if (_playerCharacter.HealthController.IsAlive && !Mathf.Approximately(health, maxHealth))
            {
                _gameManager.VignetteController.Flash();
            }
        }
    }
}
