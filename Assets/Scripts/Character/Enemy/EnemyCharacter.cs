using System;
using Character.Common;
using Character.Player;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyCharacter : BaseCharacter, ITargetable
    {
        public Transform TargetTransform => transform;
        public bool IsAlive => true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCharacter playerCharacter))
            {
                playerCharacter.HealthController.TakeDamage(100);
            }
        }
    }
}
