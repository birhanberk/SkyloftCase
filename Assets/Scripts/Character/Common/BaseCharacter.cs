using Character.Common.Controller;
using UnityEngine;

namespace Character.Common
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField] protected BaseCharacterData baseCharacterData;
        [SerializeField] private CharacterHealthController characterHealthController;

        public BaseCharacterData CharacterData => baseCharacterData;
        public CharacterHealthController HealthController => characterHealthController;
        
        protected virtual void Start()
        {
            characterHealthController.OnStart(this);
        }
    }
}
