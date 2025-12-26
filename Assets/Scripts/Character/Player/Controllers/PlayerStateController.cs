using System;
using System.Collections.Generic;
using Character.Common;
using Character.Common.States;
using Character.Player.States;
using Character.Player.States.CombatState;
using Character.Player.States.MovementState;

namespace Character.Player.Controllers
{
    [Serializable]
    public class PlayerStateController
    {
        private PlayerCharacter _playerCharacter;
        
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

            _playerCharacter.HealthController.OnDeadAction += OnDead;
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
            ChangeMovementState(PlayerStateType.Dead);
        }
    }
}
