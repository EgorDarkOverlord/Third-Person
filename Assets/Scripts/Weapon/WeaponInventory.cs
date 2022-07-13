using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public Weapon CurrentWeapon { get => _currentWeaponSlot; }
    public Ammo CurrentAmmo { get; private set; }

    public Transform WeaponHolder;
    [SerializeField] private Weapon[] _weaponSlots;
    [SerializeField] private List<Ammo> _ammoList;
    private Weapon _currentWeaponSlot;
    private int _currentWeaponIndex;

    [SerializeField]
    private Transform _dropWeaponTransform;

    public event Action<WeaponEventArgs> OnWeaponAdded;
    public event Action<WeaponEventArgs> OnAmmoAdded;
    public event Action<WeaponEventArgs> OnWeaponDropped;



    public void AddAmmo(Ammo ammo)
    {
        Ammo ammoInInventory = _ammoList.Find(_ammo => _ammo.Type == ammo.Type);
        ammoInInventory.Count += ammo.Count;

        OnAmmoAdded?.Invoke(
            new WeaponEventArgs
            {
                CurrentWeapon = CurrentWeapon,
                CurrentAmmo = ammoInInventory,
                AmmoInMagazine = CurrentWeapon != null ? CurrentWeapon.Ammo : 0,
                AmmoInInventory = ammoInInventory.Count
            }
        );
    }

    public int GetAmmoForReload()
    {
        int requiredAmmo = CurrentWeapon.ShootingInfo.MaxAmmo - CurrentWeapon.Ammo;
        if (CurrentAmmo.Count > requiredAmmo)
        {
            CurrentAmmo.Count -= requiredAmmo;
            return requiredAmmo;
        }
        else
        {
            int lastBullets = CurrentAmmo.Count;
            CurrentAmmo.Count = 0;
            return lastBullets;
        }
    }

    public void AddWeapon(WeaponPickup weaponPickup)
    {
        if (_currentWeaponSlot == null)
        {

            //Weapon weapon = Instantiate(weaponPickup.Weapon, Vector3.zero, Quaternion.identity, WeaponHolder);
            //weaponPickup.Weapon.transform.SetParent(WeaponHolder);
            //_weaponSlots[_currentWeaponIndex] = weaponPickup.Weapon;
            //_weaponSlots[_currentWeaponIndex].transform.SetParent(WeaponHolder);
            //weaponPickup.Weapon.transform.position = Vector3.zero;
            weaponPickup.Weapon.transform.position = new Vector3(1, 1, 1);
            Destroy(weaponPickup.gameObject);
        }
    }

    public bool TryAddWeapon(Weapon weapon)
    {
        if (_currentWeaponSlot == null)
        {
            _weaponSlots[_currentWeaponIndex] = weapon;
            _weaponSlots[_currentWeaponIndex].transform.SetParent(WeaponHolder);
            _weaponSlots[_currentWeaponIndex].gameObject.transform.localPosition = Vector3.zero;
            _weaponSlots[_currentWeaponIndex].gameObject.transform.localRotation = Quaternion.identity;

            SelectWeapon(_currentWeaponIndex);
            OnWeaponAdded?.Invoke(
                new WeaponEventArgs
                {
                    SlotIndex = _currentWeaponIndex,
                    CurrentWeapon = CurrentWeapon,
                    AmmoInMagazine = CurrentWeapon.Ammo,
                    AmmoInInventory = CurrentAmmo.Count
                }
            );

            return true;
        }
        return false;
    }

    public void DropWeapon()
    {
        if (_currentWeaponSlot != null)
        {
            CurrentAmmo.Count += CurrentWeapon.Ammo;
            Destroy(CurrentWeapon.gameObject);
            Instantiate(_currentWeaponSlot.PickupInfo.UnarmedWeaponPrefab, _dropWeaponTransform.position, Quaternion.identity);
            _weaponSlots[_currentWeaponIndex] = null;

            SelectWeapon(_currentWeaponIndex);
            OnWeaponDropped?.Invoke(new WeaponEventArgs());
        }
    }

    public void SelectWeapon(int index)
    {
        _currentWeaponSlot = _weaponSlots[index];
        _currentWeaponIndex = index;

        if (CurrentWeapon != null)
        {
            CurrentAmmo = _ammoList.Find(ammo => CurrentWeapon.ShootingInfo.RequiredAmmoType == ammo.Type);
        }
    }
}