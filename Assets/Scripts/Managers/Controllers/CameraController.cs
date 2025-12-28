using System;
using UnityEngine;

namespace Managers.Controllers
{
    [Serializable]
    public class CameraController
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Vector3 offset = new (0f, 5f, -8f);

        [SerializeField] private float positionSmoothTime = 0.15f;

        private Transform _target;
        private Vector3 _velocity;

        public Camera Camera => mainCamera;

        public void Setup(Transform target)
        {
            _target = target;
        }

        public void OnLevelEnd()
        {
            mainCamera.transform.localPosition = offset;
        }

        public void OnLateUpdate()
        {
            if (!_target)
                return;

            FollowPosition();
        }

        private void FollowPosition()
        {
            var desiredPosition = _target.position + offset;
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, desiredPosition, ref _velocity, positionSmoothTime);
        }
    }
}
