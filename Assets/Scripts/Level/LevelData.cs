using UnityEngine;

namespace Level
{
    [CreateAssetMenu(menuName = "Data / Level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private GameObject levelPrefab;

        public GameObject LevelPrefab => levelPrefab;
    }
}