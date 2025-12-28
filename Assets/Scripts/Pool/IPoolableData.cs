using UnityEngine;

namespace Pool
{
    public interface IPoolableData
    {
        GameObject Prefab { get; }
        int InitialSize { get; }
    }
}