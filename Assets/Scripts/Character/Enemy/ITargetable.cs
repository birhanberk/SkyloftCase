using UnityEngine;

namespace Character.Enemy
{
    public interface ITargetable
    {
        Transform TargetTransform { get; }
        Transform HitTransform { get; }
        bool IsAlive { get; }
        
        void TakeDamage(float damage);
    }
}
