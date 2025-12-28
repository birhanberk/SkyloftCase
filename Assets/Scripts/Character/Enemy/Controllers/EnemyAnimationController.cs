using System;
using UnityEngine;

namespace Character.Enemy.Controllers
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int IdleHash  = Animator.StringToHash("Idle");
        private static readonly int WalkHash  = Animator.StringToHash("Walk");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int DeathHash  = Animator.StringToHash("Death");

        public Action OnAttackEventTriggered;
        public Action OnDeadEventTriggered;
        
        public void PlayIdle()
        {
            animator.CrossFade(IdleHash, 0.1f, 0);
        }
        
        public void PlayWalk()
        {
            animator.CrossFade(WalkHash, 0.1f, 0);
        }

        public void PlayAttack()
        {
            animator.CrossFade(AttackHash, 0.1f, 0);
        }

        public void PlayDeath()
        {
            animator.CrossFade(DeathHash, 0.1f, 0);
        }

        public void OnAttackEvent()
        {
            OnAttackEventTriggered?.Invoke();
        }

        public void OnDeadEvent()
        {
            OnDeadEventTriggered?.Invoke();
        }
    }
}
