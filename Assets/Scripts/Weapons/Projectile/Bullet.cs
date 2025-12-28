using System.Collections;
using Character.Enemy;
using Pool;
using UnityEngine;
using VContainer;

namespace Weapons.Projectile
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] private BulletData bulletData;
        
        [Inject] private PoolManager _poolManager;
        
        private Coroutine _moveRoutine;
        
        public void OnExitPool()
        {
            
        }

        public void OnEnterPool()
        {
            if (_moveRoutine != null)
            {
                StopCoroutine(_moveRoutine);
            }
        }

        public void Fire(Transform firePoint, ITargetable target, float damage)
        {
            if (target != null)
            {
                transform.position = firePoint.position;
                var targetPosition = target.HitTransform.position;
                transform.rotation = Quaternion.LookRotation(targetPosition - firePoint.position);
                if (_moveRoutine != null)
                    StopCoroutine(_moveRoutine);

                _moveRoutine = StartCoroutine(MoveToTarget(targetPosition, target, damage));
            }
        }
        
        private IEnumerator MoveToTarget(Vector3 targetPosition, ITargetable target, float damage)
        {
            while (true)
            {
                var toTarget = targetPosition - transform.position;
                var distance = toTarget.magnitude;

                if (distance <= 0.01f)
                    break;

                var direction = toTarget / distance;
                var step = bulletData.Speed * Time.deltaTime;

                if (step >= distance)
                {
                    transform.position = targetPosition;
                    break;
                }

                transform.position += direction * step;
                transform.rotation = Quaternion.LookRotation(direction);

                yield return null;
            }
            target.TakeDamage(damage);
            _poolManager.Release(bulletData, this);
            _moveRoutine = null;
        }
    }
}