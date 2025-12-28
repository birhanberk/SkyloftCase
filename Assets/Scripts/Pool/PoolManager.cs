using System;
using System.Collections.Generic;
using Character.Enemy;
using UnityEngine;
using VContainer;
using Weapons.Projectile;

namespace Pool
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private Transform enemyParent;
        [SerializeField] private List<EnemyData> enemyData = new ();
        
        [SerializeField] private Transform bulletParent;
        [SerializeField] private List<BulletData> bulletData = new ();
        
        [Inject] private IObjectResolver _objectResolver;

        private ObjectPool<EnemyCharacter> _enemyPool;
        private ObjectPool<Bullet> _projectilePool;

        private readonly Dictionary<Type, object> _pools = new();

        public void Start()
        {
            _enemyPool = new ObjectPool<EnemyCharacter>(enemyData, enemyParent);
            _objectResolver.Inject(_enemyPool);
            _projectilePool = new ObjectPool<Bullet>(bulletData, bulletParent);
            _objectResolver.Inject(_projectilePool);

            _pools.Add(typeof(EnemyCharacter), _enemyPool);
            _pools.Add(typeof(Bullet), _projectilePool);
        }

        public T Get<T>(IPoolableData data) where T : Component, IPoolable
        {
            if (_pools.TryGetValue(typeof(T), out var pool))
            {
                return ((ObjectPool<T>)pool).Get(data);
            }
            return null;
        }

        public void Release<T>(IPoolableData data, T obj) where T : Component, IPoolable
        {
            if (_pools.TryGetValue(typeof(T), out var pool))
            {
                ((ObjectPool<T>)pool).Release(data, obj);
            }
        }
    }
}