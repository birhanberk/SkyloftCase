using UnityEngine;

namespace Managers
{
    public class PersistentDataManager : MonoBehaviour
    {
        private const string totalKilledKey = "TotalKilled";
        
        public string TotalKilledKey => totalKilledKey;
        
        public void Save(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int Load(string key)
        {
            return PlayerPrefs.GetInt(key, 0);
        }
    }
}
