using System;
using System.Collections.Generic;
using Character.Common;
using Character.Enemy;
using Managers;
using Pool;
using UI.Joystick;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using Random = UnityEngine.Random;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;
        
        [Inject] private PoolManager _poolManager;
        [Inject] private GameManager _gameManager;
        [Inject] private PersistentDataManager _persistentDataManager;
        [Inject] private UIManager _uiManager;
        
        private readonly List<EnemyCharacter> _activeEnemies = new();
        private int _killCount;

        private Camera _mainCamera;
        private Transform _playerTransform;
        private UIJoystick _joystick;
        
        public LevelData LevelData => levelData;
        public int KillCount => _killCount;

        public Action<int> OnEnemyKilled;

        private void Start()
        {
            _mainCamera = _gameManager.CameraController.Camera;
            _playerTransform = _gameManager.PlayerController.PlayerCharacter.transform;
            _joystick = _uiManager.Joystick;
        }

        private void Update()
        {
            HandleDespawnByDistance();
        }

        public void StartLevel()
        {
            SpawnInitialEnemies();
        }
        
        private EnemyData GetRandomEnemyByRatio()
        {
            var totalWeight = 0f;

            foreach (var data in levelData.LevelSpawnData)
                totalWeight += data.SpawnRatio;

            var random = Random.Range(0f, totalWeight);
            var current = 0f;

            foreach (var data in levelData.LevelSpawnData)
            {
                current += data.SpawnRatio;
                if (random <= current)
                    return data.EnemyData;
            }

            return levelData.LevelSpawnData[0].EnemyData;
        }
        
        private Vector3 GetSpawnPosition()
        {
            const int maxTryCount = 12;

            var bestCandidate = Vector3.zero;
            var bestScore = float.MinValue;

            var joystickDir = GetJoystickDirection(out var hasDirection);

            for (var i = 0; i < maxTryCount; i++)
            {
                if (!TryGetNavMeshCandidate(out var hit))
                    continue;

                var score = CalculateScore(hit.position, joystickDir, hasDirection);

                if (score <= bestScore)
                    continue;

                bestScore = score;
                bestCandidate = hit.position;
            }

            return bestCandidate != Vector3.zero
                ? bestCandidate
                : GetFallbackPosition();
        }
        
        private Vector3 GetJoystickDirection(out bool hasDirection)
        {
            var dir = new Vector3(
                _joystick.Direction.x,
                0f,
                _joystick.Direction.y
            );

            hasDirection = dir.sqrMagnitude > 0.01f;

            if (hasDirection)
                dir.Normalize();

            return dir;
        }
        
        private bool TryGetNavMeshCandidate(out NavMeshHit hit)
        {
            const float navMeshSampleRadius = 2.5f;

            var candidate = GetCameraBasedSpawnPosition();
            return NavMesh.SamplePosition(candidate, out hit, navMeshSampleRadius, NavMesh.AllAreas);
        }
        
        private float CalculateScore(Vector3 spawnPos, Vector3 joystickDir, bool hasDirection)
        {
            const float directionBiasThreshold = 0.2f;

            var delta = spawnPos - _playerTransform.position;

            if (delta.sqrMagnitude < 0.001f)
                return float.MinValue;

            if (!hasDirection)
                return Random.value;

            var dirToSpawn = delta.normalized;
            var dot = Vector3.Dot(joystickDir, dirToSpawn);

            if (dot < directionBiasThreshold)
                return float.MinValue;

            return dot + Random.Range(-0.15f, 0.15f);
        }

        
        private Vector3 GetFallbackPosition()
        {
            NavMesh.SamplePosition(
                _playerTransform.position,
                out var hit,
                6f,
                NavMesh.AllAreas);

            return hit.position;
        }
        
        private Vector3 GetCameraBasedSpawnPosition()
        {
            var cam = _mainCamera;

            float x;
            float y;

            var side = Random.Range(0, 4);

            switch (side)
            {
                case 0:
                    x = Random.Range(-0.2f, 0f);
                    y = Random.Range(0f, 1f);
                    break;
                case 1:
                    x = Random.Range(1f, 1.2f);
                    y = Random.Range(0f, 1f);
                    break;
                case 2:
                    x = Random.Range(0f, 1f);
                    y = Random.Range(-0.2f, 0f);
                    break;
                default:
                    x = Random.Range(0f, 1f);
                    y = Random.Range(1f, 1.2f);
                    break;
            }

            var ray = cam.ViewportPointToRay(new Vector3(x, y, cam.nearClipPlane));
            var plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out var distance))
                return ray.GetPoint(distance);

            return cam.transform.position;
        }

        private void SpawnEnemy()
        {
            if (_activeEnemies.Count >= levelData.MaxActiveEnemyCount)
                return;

            var enemyData = GetRandomEnemyByRatio();
            var enemy = _poolManager.Get<EnemyCharacter>(enemyData);

            enemy.MovementController.SetOnLevel(GetSpawnPosition());
            enemy.HealthController.OnDeadAction += OnEnemyDead;

            _activeEnemies.Add(enemy);
        }

        private void SpawnInitialEnemies()
        {
            for (var i = 0; i < levelData.MaxActiveEnemyCount; i++)
                SpawnEnemy();
        }

        private void OnEnemyDead(BaseCharacter character)
        {
            if (character is EnemyCharacter enemy)
            {
                _killCount++;
                OnEnemyKilled?.Invoke(_killCount);
                enemy.HealthController.OnDeadAction -= OnEnemyDead;
                
                _activeEnemies.Remove(enemy);

                SpawnEnemy();
            }
        }
        
        private void HandleDespawnByDistance()
        {
            var cam = _mainCamera;
            var margin = levelData.DespawnDistance;

            for (var i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                var enemy = _activeEnemies[i];

                var viewportPos = cam.WorldToViewportPoint(enemy.transform.position);

                if (viewportPos.z < 0f)
                    continue;

                var isOutside =
                    viewportPos.x < -margin ||
                    viewportPos.x > 1f + margin ||
                    viewportPos.y < -margin ||
                    viewportPos.y > 1f + margin;

                if (!isOutside)
                    continue;

                enemy.HealthController.OnDeadAction -= OnEnemyDead;
                _activeEnemies.RemoveAt(i);
                _poolManager.Release((EnemyData)enemy.CharacterData, enemy);

                SpawnEnemy();
            }
        }
        
        public void OnCompleted()
        {
            foreach (var enemy in _activeEnemies)
            {
                enemy.HealthController.OnDeadAction = null;
                _poolManager.Release((EnemyData)enemy.CharacterData, enemy);
                enemy.HealthController.OnDeadAction -= OnEnemyDead;
            }

            _activeEnemies.Clear();
        }
    }
}
