using System;
using Character.Player;
using Managers;
using UnityEngine;
using VContainer;

namespace Character.Enemy.Controllers
{
    [Serializable]
    public class EnemyTargetController
    {
        [SerializeField] private Transform hitTransform;
        
        [Inject] private GameManager _gameManager;

        private EnemyCharacter _enemyCharacter;
        
        public Transform HitTransform => hitTransform;
        public PlayerCharacter Target => _gameManager.PlayerController.PlayerCharacter;

        public void OnStart(EnemyCharacter enemyCharacter)
        {
            _enemyCharacter = enemyCharacter;
        }

        public bool IsInAttackRange()
        {
            return Vector3.Distance(Target.transform.position, _enemyCharacter.transform.position) <= ((EnemyData)_enemyCharacter.CharacterData).AttackRange;
        }

        public void Attack()
        {
            Target.HealthController.TakeDamage(((EnemyData)_enemyCharacter.CharacterData).AttackDamage);
        }
    }
}
