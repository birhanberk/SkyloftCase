using UnityEngine;

namespace Character.Common
{
    public class BaseCharacterData : ScriptableObject
    {
        [SerializeField] private int health;
        [SerializeField] private float moveSpeed;

        public int Health => health;
        public float MoveSpeed => moveSpeed;
    }
}
