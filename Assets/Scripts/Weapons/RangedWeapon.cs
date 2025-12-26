using UnityEngine;

namespace Weapons
{
    public class RangedWeapon : BaseWeapon
    {
        [SerializeField] private Transform leftHandPoint;
        [SerializeField] private Transform firePoint;

        public Transform LeftHandPoint => leftHandPoint;

        public override void Attack()
        {

        }
    }
}
