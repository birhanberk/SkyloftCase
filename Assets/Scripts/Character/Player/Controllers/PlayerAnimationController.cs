using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Character.Player.Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform aimIKTarget;
        [SerializeField] private Rig rig;

        private PlayerCharacter _playerCharacter;

        private bool _isActive = true;

        private static readonly int EmptyHash  = Animator.StringToHash("Empty");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int DeathHash  = Animator.StringToHash("Death");

        private static readonly int MoveXHash  = Animator.StringToHash("X");
        private static readonly int MoveYHash  = Animator.StringToHash("Y");
        private static readonly int SpeedHash  = Animator.StringToHash("Speed");

        public Action OnAttackEventTriggered;
        public Action OnDeadEventTriggered;

        public void OnStart(PlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;
        }

        public void OnLevelStart()
        {
            SetActive(true);
            StartCoroutine(UpdateRigWeight(0, 1));
        }

        public void SetIKTarget(Transform target)
        {
            aimIKTarget. position = target.position;
        }

        public void UpdateLocomotion(Vector2 input, float speed)
        {
            if (!_isActive)
                return;

            animator.SetFloat(MoveXHash, input.x, 0.1f, Time.deltaTime);
            animator.SetFloat(MoveYHash, input.y, 0.1f, Time.deltaTime);

            speed = ClampToMinWalkSpeed(speed);

            animator.SetFloat(SpeedHash, Mathf.Clamp01(speed), 0.1f, Time.deltaTime);
        }

        private float ClampToMinWalkSpeed(float speed)
        {
            var move = _playerCharacter.MovementController;

            if (speed > move.WalkThreshold && speed < move.RunThreshold)
            {
                speed = move.RunThreshold;
            }

            return speed;
        }

        public void PlaySearch()
        {
            animator.CrossFade(EmptyHash, 0.1f, 1);
        }

        public void PlayAttack()
        {
            animator.CrossFade(AttackHash, 0.1f, 1);
        }

        public void PlayDeath()
        {
            SetActive(false);
            StartCoroutine(UpdateRigWeight(1, 0));
            animator.SetLayerWeight(1, 0);
            animator.CrossFade(DeathHash, 0.1f);
        }

        public void OnAttackEvent()
        {
            OnAttackEventTriggered?.Invoke();
        }

        public void OnDeadEvent()
        {
            OnDeadEventTriggered?.Invoke();
        }

        private void SetActive(bool active)
        {
            _isActive = active;
        }
        
        private IEnumerator UpdateRigWeight(float start, float end)
        {
            float elapsedTime = 0;

            while (elapsedTime < 0.1f)
            {
                rig.weight = Mathf.Lerp(start, end, (elapsedTime / 0.1f));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rig.weight = end;
        }
    }
}