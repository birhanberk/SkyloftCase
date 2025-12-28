using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "Data / Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private float damage;
        [SerializeField] private float range;

        public float Damage => damage;
        public float Range => range;
    }
}
