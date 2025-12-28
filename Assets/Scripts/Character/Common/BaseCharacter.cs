using Character.Common.Controller;
using UnityEngine;
using VContainer;

namespace Character.Common
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField] protected BaseCharacterData baseCharacterData;
        [SerializeField] private CharacterHealthController characterHealthController;

        public BaseCharacterData CharacterData => baseCharacterData;
        public CharacterHealthController HealthController => characterHealthController;

        [Inject]
        private void Construct()
        {
            characterHealthController.OnStart(this);
        }
    }
}
