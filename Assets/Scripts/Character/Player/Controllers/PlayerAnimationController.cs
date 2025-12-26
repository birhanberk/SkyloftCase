using UnityEngine;

namespace Character.Player.Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private PlayerCharacter _playerCharacter;

        private Transform _leftHandIKTarget;
        private float _leftHandIKWeight;

        private Transform _lookTarget;
        private bool _enableLookAt;

        private bool _isActive = true;

        private static readonly int EmptyHash  = Animator.StringToHash("Empty");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int DeathHash  = Animator.StringToHash("Death");

        private static readonly int MoveXHash  = Animator.StringToHash("X");
        private static readonly int MoveYHash  = Animator.StringToHash("Y");
        private static readonly int SpeedHash  = Animator.StringToHash("Speed");

        public void OnStart(PlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;
            SetActive(true);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!_isActive)
                return;

            HandleLeftHandIK();
            HandleLookAt();
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
            DisableLookAt();
            EnableLeftHandIK(_leftHandIKTarget);
        }

        public void PlayAttack()
        {
            animator.CrossFade(AttackHash, 0.1f, 1);
            DisableLeftHandIK();
            EnableLookAt(_lookTarget);
        }

        public void PlayDeath()
        {
            SetActive(false);
            animator.CrossFade(DeathHash, 0.1f);
        }

        public void EnableLeftHandIK(Transform target, float weight = 1f)
        {
            _leftHandIKTarget = target;
            _leftHandIKWeight = Mathf.Clamp01(weight);
        }

        private void DisableLeftHandIK()
        {
            _leftHandIKTarget = null;
            _leftHandIKWeight = 0f;
        }

        private void HandleLeftHandIK()
        {
            if (!_leftHandIKTarget || _leftHandIKWeight <= 0f)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
                return;
            }

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftHandIKWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _leftHandIKWeight);

            animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandIKTarget.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandIKTarget.rotation);
        }

        public void EnableLookAt(Transform target)
        {
            _lookTarget = target;
            _enableLookAt = target;
        }

        private void DisableLookAt()
        {
            _enableLookAt = false;
        }

        private void HandleLookAt()
        {
            if (!_enableLookAt || !_lookTarget)
            {
                animator.SetLookAtWeight(0f);
                return;
            }

            animator.SetLookAtWeight(1f, 0.7f, 0.2f, 0f, 0.5f);
            animator.SetLookAtPosition(_lookTarget.position);
        }

        private void SetActive(bool active)
        {
            _isActive = active;

            if (!active)
            {
                DisableLeftHandIK();
                DisableLookAt();
            }
        }
    }
}