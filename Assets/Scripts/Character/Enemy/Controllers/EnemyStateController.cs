using System;
using System.Collections.Generic;
using Character.Common;
using Character.Common.States;
using Character.Enemy.States;

namespace Character.Enemy.Controllers
{
    [Serializable]
    public class EnemyStateController
    {
        private EnemyCharacter _enemyCharacter;
        private Dictionary<EnemyStateType, BaseEnemyState> _stateMap = new ();

        private CharacterStateMachine _stateMachine;

        public void OnStart(EnemyCharacter enemyCharacter)
        {
            _enemyCharacter = enemyCharacter;

            CreateStateMachine();
            CreateStates();
            
            ChangeState(EnemyStateType.Idle);
        }

        public void OnExitPool()
        {
            ChangeState(EnemyStateType.Idle);
            _enemyCharacter.HealthController.OnDeadAction += OnDead;
        }
        
        public void OnUpdate()
        {
            _stateMachine.Tick();
        }
        
        private void CreateStateMachine()
        {
            _stateMachine = new CharacterStateMachine();
        }

        private void CreateStates()
        {
            _stateMap.Add(EnemyStateType.Idle, new EnemyIdleState(_enemyCharacter));
            _stateMap.Add(EnemyStateType.Walk, new EnemyWalkState(_enemyCharacter));
            _stateMap.Add(EnemyStateType.Attack, new EnemyAttackState(_enemyCharacter));
            _stateMap.Add(EnemyStateType.Dead, new EnemyDeadState(_enemyCharacter));
        }
        
        public void ChangeState(EnemyStateType stateType)
        {
            if (_stateMap.TryGetValue(stateType, out var state))
            {
                _stateMachine.ChangeState(state);
            }
        }
        
        private void OnDead(BaseCharacter character)
        {
            _enemyCharacter.HealthController.OnDeadAction -= OnDead;
            ChangeState(EnemyStateType.Dead);
        }
    }
}
