using Pool;
using UnityEngine;

namespace Weapons.Projectile
{
    [CreateAssetMenu(menuName = "Data / Bullet")]
    public class BulletData : ScriptableObject, IPoolableData
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialSize;
        [SerializeField] private float speed = 15f;

        public GameObject Prefab => prefab;
        public int InitialSize => initialSize;
        public float Speed => speed;
    }
}
