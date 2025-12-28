using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Weapons;

namespace Character.Player.Controllers
{
    [Serializable]
    public class PlayerWeaponController
    {
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private BaseWeapon startingWeapon;
        
        [Inject] private IObjectResolver _objectResolver;

        private PlayerCharacter _playerCharacter;
        private readonly Dictionary<WeaponData, BaseWeapon> _ownedWeapons = new();
        private BaseWeapon _currentWeapon;

        public bool CanAttack => _currentWeapon.CanAttack(_playerCharacter.TargetController.GetClosestTarget());

        public void OnStart(PlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;
            EquipStartingWeapon();
        }

        private void EquipStartingWeapon()
        {
            if (!startingWeapon)
                return;

            _objectResolver.Inject(startingWeapon);
            AddWeapon(startingWeapon);
            EquipWeapon(startingWeapon.Data);
        }

        public void AddWeapon(BaseWeapon weapon)
        {
            var weaponData = weapon.Data;

            if (!_ownedWeapons.TryAdd(weaponData, weapon))
                return;

            weapon.transform.SetParent(weaponHolder);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localEulerAngles = Vector3.zero;
            weapon.gameObject.SetActive(false);
        }

        public void EquipWeapon(WeaponData weaponData)
        {
            if (!_ownedWeapons.TryGetValue(weaponData, out var weapon))
                return;

            _currentWeapon?.OnUnequip();
            _currentWeapon = weapon;
            _currentWeapon.OnEquip();
        }

        public void Attack()
        {
            _currentWeapon?.Attack(_playerCharacter.TargetController.GetClosestTarget());
        }
    }
}
