using Character.Common;
using Pool;
using UnityEngine;

namespace Character.Enemy
{
    [CreateAssetMenu(menuName = "Data / Enemy")]
    public class EnemyData : BaseCharacterData, IPoolableData
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialSize;
        
        [SerializeField] private float attackDamage;
        [SerializeField] private float attackRange;

        public GameObject Prefab => prefab;
        public int InitialSize => initialSize;
        public float AttackDamage => attackDamage;
        public float AttackRange => attackRange;
    }
}
