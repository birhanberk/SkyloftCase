using System;
using UnityEngine;
using UnityEngine.AI;

namespace Character.Enemy.Controllers
{
    [Serializable]
    public class EnemyMovementController
    {
        [SerializeField] private NavMeshAgent navMeshAgent;

        private EnemyCharacter _enemyCharacter;

        public void OnStart(EnemyCharacter enemyCharacter)
        {
            _enemyCharacter = enemyCharacter;
            navMeshAgent.speed = _enemyCharacter.CharacterData.MoveSpeed;
        }
        
        public void MoveToTarget()
        {
            if (navMeshAgent.enabled && navMeshAgent.gameObject.activeInHierarchy)
            {
                navMeshAgent.SetDestination(_enemyCharacter.TargetController.Target.transform.position);
            }
        }

        public void Stop()
        {
            if (!navMeshAgent.enabled || !navMeshAgent.gameObject.activeInHierarchy)
                return;
            
            navMeshAgent.isStopped = true;
            navMeshAgent.ResetPath();
            navMeshAgent.velocity = Vector3.zero;
        }

        public void OnExitPool()
        {

        }

        public void SetOnLevel(Vector3 position)
        {
            _enemyCharacter.transform.position = position;
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
        }

        public void OnEnterPool()
        {
            Stop();
            navMeshAgent.enabled = false;
        }
    }
}
