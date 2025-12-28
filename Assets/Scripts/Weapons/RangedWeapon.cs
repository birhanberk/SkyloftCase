using Character.Enemy;
using Pool;
using UnityEngine;
using VContainer;
using Weapons.Projectile;

namespace Weapons
{
    public class RangedWeapon : BaseWeapon
    {
        [SerializeField] private BulletData bulletData;
        [SerializeField] private Transform firePoint;
        [SerializeField] private ParticleSystem muzzleEffect;

        [Inject] private PoolManager _poolManager;

        public override void Attack(ITargetable target)
        {
            var bullet = _poolManager.Get<Bullet>(bulletData);
            bullet.Fire(firePoint, target, weaponData.Damage);
            muzzleEffect.Play();
        }

        public override bool CanAttack(ITargetable target)
        {
            return Vector3.Distance(target.HitTransform.position, firePoint.transform.position) < weaponData.Range;
        }
    }
}
