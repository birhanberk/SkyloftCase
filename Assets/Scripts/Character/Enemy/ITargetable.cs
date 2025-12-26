using UnityEngine;

namespace Character.Enemy
{
    public interface ITargetable
    {
        Transform TargetTransform { get; }
        bool IsAlive { get; }
    }
}
