using System;
using Managers;
using UnityEngine;
using VContainer;

namespace Character.Player.Controllers
{
    [Serializable]
    public class PlayerMovementController
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float runThreshold = 0.5f;
        [SerializeField] private float walkThreshold = 0.01f;
        [SerializeField] private float rotationSmoothSpeed = 12f;
        
        [Inject] private UIManager _uiManager;

        private PlayerCharacter _playerCharacter;
        private Vector3 _moveDirection;
        
        public Vector2 InputDirection => _uiManager.Joystick.Direction;
        public float Speed => _moveDirection.magnitude;
        public bool HasInput => InputDirection.sqrMagnitude > walkThreshold;
        public bool ShouldRun => InputDirection.magnitude >= runThreshold;

        public float RunThreshold => runThreshold;
        public float WalkThreshold => walkThreshold;
        
        public void OnStart(PlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;
        }

        public void OnUpdate()
        {
            MoveCharacter();
        }
        
        public void SetMovement(Vector2 input)
        {
            _moveDirection = new Vector3(input.x, 0f, input.y);

            var desiredForward = Vector3.zero;

            if (_playerCharacter.TargetController.HasTarget)
            {
                var target = _playerCharacter.TargetController.GetClosestTarget();
                if (target != null)
                {
                    desiredForward =
                        target.TargetTransform.position - _playerCharacter.transform.position;
                }
            }
            else if (_moveDirection.sqrMagnitude > walkThreshold)
            {
                desiredForward = _moveDirection;
            }

            if (desiredForward.sqrMagnitude > 0.001f)
            {
                desiredForward.y = 0f;
                desiredForward.Normalize();

                SmoothRotate(desiredForward);
            }
        }

        public void ApplyInput()
        {
            SetMovement(InputDirection);
        }

        public Vector2 GetLocalMoveInput()
        {
            if (_moveDirection.sqrMagnitude < 0.001f)
                return Vector2.zero;

            var localDir = _playerCharacter.transform.InverseTransformDirection(_moveDirection.normalized);

            return new Vector2(localDir.x, localDir.z);
        }
        
        public void StopImmediately()
        {
            _moveDirection = Vector3.zero;
            if (characterController && characterController.enabled)
            {
                characterController.Move(Vector3.zero);
            }
            characterController.enabled = false;
        }

        private void MoveCharacter()
        {
            if (characterController.enabled)
            {
                var move = _moveDirection * ((PlayerData)_playerCharacter.CharacterData).MoveSpeed;
                characterController.Move(move * Time.deltaTime);
            }
        }
        
        private void SmoothRotate(Vector3 desiredForward)
        {
            var targetRotation = Quaternion.LookRotation(desiredForward, Vector3.up);

            _playerCharacter.transform.rotation = Quaternion.Slerp(_playerCharacter.transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
        }
    }
}
