using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "Data / Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private float attackRate;
        [SerializeField] private float damage;
        [SerializeField] private float range;

        public float AttackRate => attackRate;
        public float Damage => damage;
        public float Range => range;
    }
}
