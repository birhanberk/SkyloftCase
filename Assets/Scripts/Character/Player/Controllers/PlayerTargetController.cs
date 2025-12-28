using System;
using System.Collections.Generic;
using Character.Enemy;
using UnityEngine;

namespace Character.Player.Controllers
{
    [Serializable]
    public class PlayerTargetController : MonoBehaviour
    {
        [SerializeField] private SphereCollider triggerCollider;
        [SerializeField] private LayerMask targetLayer;

        private readonly HashSet<ITargetable> _targets = new();

        public bool HasTarget => _targets.Count > 0;

        public void OnLevelStart()
        {
            ClearTargets();
            SetDetection(true);
        }
        
        public void SetDetection(bool enable)
        {
            triggerCollider.enabled = enable;
        }
        
        public void ClearTargets()
        {
            _targets.Clear();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & targetLayer.value) == 0)
                return;
            
            if (other.TryGetComponent(out ITargetable target))
            {
                if (target.IsAlive)
                {
                    _targets.Add(target);
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ITargetable target))
            {
                _targets.Remove(target);
            }
        }

        public ITargetable GetClosestTarget()
        {
            var minSqrDistance = float.MaxValue;
            ITargetable closest = null;
            var deadTargets = new List<ITargetable>();
            foreach (var target in _targets)
            {
                if (!target.IsAlive)
                {
                    deadTargets.Add(target);
                    continue;
                }

                var sqrDist = (target.TargetTransform.position - transform.position).sqrMagnitude;

                if (sqrDist < minSqrDistance)
                {
                    minSqrDistance = sqrDist;
                    closest = target;
                }
            }

            foreach (var target in deadTargets)
            {
                _targets.Remove(target);
            }
            return closest;
        }

        public void RemoveTarget(ITargetable target)
        {
            _targets.Remove(target);
        }
    }
}