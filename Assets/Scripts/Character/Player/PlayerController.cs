using System;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace Character.Player
{
    [Serializable]
    public class PlayerController
    {
        [SerializeField] private Transform sceneParent;
        [SerializeField] private PlayerCharacter playerPrefab;

        [Inject] private IObjectResolver _objectResolver;
        
        public PlayerCharacter PlayerCharacter { get; private set; }
        
        public void CreatePlayer()
        {
            PlayerCharacter = Object.Instantiate(playerPrefab, sceneParent);
            _objectResolver.Inject(PlayerCharacter);
            Hide();
        }

        public void DestroyPlayer()
        {
            Object.Destroy(PlayerCharacter.gameObject);
        }

        public void Show()
        {
            PlayerCharacter.gameObject.SetActive(true);
        }

        public void Hide()
        {
            PlayerCharacter.gameObject.SetActive(false);
        }
    }
}
