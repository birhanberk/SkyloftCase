using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Pool
{
    public class ObjectPool<T> where T : Component
    {
        private readonly Transform _parent;
        private readonly Dictionary<IPoolableData, Stack<T>> _pool = new();
        private readonly IEnumerable<IPoolableData> _poolableData;
        
        private IObjectResolver _objectResolver;

        public ObjectPool(IEnumerable<IPoolableData> poolableData, Transform parent)
        {
            _parent = parent;
            _poolableData = poolableData;
        }

        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            
            foreach (var so in _poolableData)
            {
                var stack = new Stack<T>();

                for (var i = 0; i < so.InitialSize; i++)
                {
                    stack.Push(CreateNew(so));
                }

                _pool.Add(so, stack);
            }
        }

        private T CreateNew(IPoolableData data)
        {
            var go = Object.Instantiate(data.Prefab, _parent);
            var obj = go.GetComponent<T>();
            _objectResolver.Inject(obj);

            obj.gameObject.SetActive(false);
            return obj;
        }

        public T Get(IPoolableData data)
        {
            if (!_pool.TryGetValue(data, out var stack))
            {
                stack = new Stack<T>();
                _pool.Add(data, stack);
            }

            var obj = stack.Count > 0 ? stack.Pop() : CreateNew(data);
            obj.gameObject.SetActive(true);

            if (obj.TryGetComponent(out IPoolable poolable))
                poolable.OnExitPool();
            return obj;
        }

        public void Release(IPoolableData data, T obj)
        {
            if (obj.TryGetComponent(out IPoolable poolable))
                poolable.OnEnterPool();

            obj.gameObject.SetActive(false);

            if (!_pool.TryGetValue(data, out var stack))
            {
                stack = new Stack<T>();
                _pool.Add(data, stack);
            }
            stack.Push(obj);
        }
    }
}
