
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;
using System.Collections;

namespace Player
{
    public class PlayerShooting : CharacterShooting
    {
        new public event Action<WeaponEventArgs> OnWeaponSelected;
        new public event Action<WeaponEventArgs> OnReload;

        private CinemachineImpulseSource _cameraShake;

        protected override void Awake()
        {
            base.Awake();

            _cameraShake = GetComponent<CinemachineImpulseSource>();
        }

        public override void SelectWeapon(int index)
        {
            _weaponInventory.SelectWeapon(index);

            if (CurrentWeapon != null)
            {
                _weaponAnimator.runtimeAnimatorController = _weaponInventory.CurrentWeapon.AnimationInfo.AnimatorOverrideController;
                _cameraShake.m_ImpulseDefinition.m_RawSignal = _weaponInventory.CurrentWeapon.ShootingInfo.NoiseSettings;

                OnWeaponSelected?.Invoke(
                    new WeaponEventArgs
                    {
                        SlotIndex = index,
                        CurrentWeapon = CurrentWeapon,
                        AmmoInMagazine = CurrentWeapon.Ammo,
                        AmmoInInventory = _weaponInventory.CurrentAmmo.Count
                    }
                );
            }
            else
            {
                OnWeaponSelected?.Invoke(
                    new WeaponEventArgs
                    {
                        SlotIndex = index,
                        CurrentWeapon = null,
                    }
                );
            }
        }

        public override void Shoot(Vector3 direction)
        {
            base.Shoot(direction);
            _cameraShake.GenerateImpulse();
        }

        public override IEnumerator Reload()
        {
            int ammoForReload = _weaponInventory.GetAmmoForReload();

            if (ammoForReload == 0)
                yield break;

            _weaponAnimator.SetTrigger("Reload");
            yield return new WaitForSeconds(0.1f); // Ожидание чтобы _weaponAnimator смог нормально переключиться на Weapon Reload
            CurrentWeapon.Reload(ammoForReload);
            yield return new WaitForSeconds(_weaponAnimator.GetCurrentAnimatorStateInfo(0).length);

            OnReload?.Invoke(
                new WeaponEventArgs
                {
                    CurrentWeapon = CurrentWeapon,
                    AmmoInMagazine = CurrentWeapon.Ammo,
                    AmmoInInventory = _weaponInventory.CurrentAmmo.Count
                }
            );
        }
    }
}
