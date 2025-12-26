using UnityEngine;

namespace Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponData weaponData;

        public WeaponData Data => weaponData;

        public void OnEquip()
        {
            gameObject.SetActive(true);
        }

        public void OnUnequip()
        {
            gameObject.SetActive(false);
        }

        public abstract void Attack();
    }
}
