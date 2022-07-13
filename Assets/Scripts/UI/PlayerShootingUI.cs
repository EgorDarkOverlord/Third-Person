using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI
{
    class PlayerShootingUI : MonoBehaviour
    {
        [SerializeField] private Player.PlayerShooting _playerShooting;

        [SerializeField] private TMPro.TMP_Text _slotText;
        [SerializeField] private TMPro.TMP_Text _ammoText;



        private void OnEnable()
        {
            _playerShooting.OnWeaponSelected += OnWeaponSelected;
            _playerShooting.OnWeaponAdded += OnWeaponAdded;
            _playerShooting.OnReload += OnReload;
            _playerShooting.OnAmmoAdded += OnAmmoAdded;
            _playerShooting.OnShoot += OnShoot;
            _playerShooting.OnWeaponDropped += OnWeaponDropped;
        }

        private void OnDisable()
        {
            _playerShooting.OnWeaponSelected -= OnWeaponSelected;
            _playerShooting.OnWeaponAdded -= OnWeaponAdded;
            _playerShooting.OnReload -= OnReload;
            _playerShooting.OnAmmoAdded -= OnAmmoAdded;
            _playerShooting.OnShoot -= OnShoot;
            _playerShooting.OnWeaponDropped -= OnWeaponDropped;
        }



        private void UpdateUI(int slotIndex, Weapon weapon, int ammoInMagazine, int ammoInInventory)
        {
            UpdateSlotText(slotIndex);
            UpdateAmmoText(weapon, ammoInMagazine, ammoInInventory);
        }

        private void UpdateSlotText(int slotIndex)
        {
            _slotText.text = $"Слот {slotIndex + 1}";
        }

        private void UpdateAmmoText(Weapon weapon, int ammoInMagazine, int ammoInInventory)
        {
            if (weapon != null)
            {
                _ammoText.text = $"{weapon.Name} {ammoInMagazine} / {ammoInInventory}";
            }
            else
            {
                _ammoText.text = $"Пусто";
            }
        }



        private void OnAmmoAdded(WeaponEventArgs weaponEventArgs)
        {
            if (weaponEventArgs.CurrentWeapon != null)
            {
                if (weaponEventArgs.CurrentWeapon.ShootingInfo.RequiredAmmoType == weaponEventArgs.CurrentAmmo.Type)
                {
                    UpdateAmmoText(weaponEventArgs.CurrentWeapon, weaponEventArgs.AmmoInMagazine, weaponEventArgs.AmmoInInventory);
                }
            }
        }

        private void OnWeaponAdded(WeaponEventArgs weaponEventArgs)
        {
            UpdateAmmoText(weaponEventArgs.CurrentWeapon, weaponEventArgs.AmmoInMagazine, weaponEventArgs.AmmoInInventory);
        }

        private void OnReload(WeaponEventArgs weaponEventArgs)
        {
            UpdateAmmoText(weaponEventArgs.CurrentWeapon, weaponEventArgs.AmmoInMagazine, weaponEventArgs.AmmoInInventory);
        }

        private void OnShoot(WeaponEventArgs weaponEventArgs)
        {
            UpdateAmmoText(weaponEventArgs.CurrentWeapon, weaponEventArgs.AmmoInMagazine, weaponEventArgs.AmmoInInventory);
        }

        private void OnWeaponSelected(WeaponEventArgs weaponEventArgs)
        {
            UpdateUI(weaponEventArgs.SlotIndex, weaponEventArgs.CurrentWeapon, weaponEventArgs.AmmoInMagazine, weaponEventArgs.AmmoInInventory);
        }

        private void OnWeaponDropped(WeaponEventArgs weaponEventArgs)
        {
            UpdateAmmoText(null, 0, 0);
        }
    }
}
