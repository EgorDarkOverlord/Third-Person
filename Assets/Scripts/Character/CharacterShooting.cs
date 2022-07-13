using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterShooting : MonoBehaviour
{
    protected WeaponInventory _weaponInventory;
    protected Rig _weaponRig;
    protected Animator _animator;
    protected Animator _weaponAnimator;
    protected int _animIDAiming;
    protected int _animIDHide;
    protected int _animLayerIDAiming;

    public bool WeaponEquipping;

    public Weapon CurrentWeapon => _weaponInventory.CurrentWeapon;

    public event Action<WeaponEventArgs> OnWeaponSelected;
    public event Action<WeaponEventArgs> OnWeaponAdded;
    public event Action<WeaponEventArgs> OnAmmoAdded;
    public event Action<WeaponEventArgs> OnReload;
    public event Action<WeaponEventArgs> OnShoot;
    public event Action<WeaponEventArgs> OnWeaponDropped;



    protected virtual void Awake()
    {
        _weaponInventory = GetComponent<WeaponInventory>();
        _animator = GetComponent<Animator>();
        _weaponRig = transform.Find("Weapon Rig").GetComponent<Rig>();
        _weaponAnimator = _weaponRig.GetComponent<Animator>();

        _animIDAiming = Animator.StringToHash("Aiming");
        _animIDHide = Animator.StringToHash("Hide");
        _animLayerIDAiming = _animator.GetLayerIndex("Aiming");
    }

    private void OnEnable()
    {
        _weaponInventory.OnWeaponAdded += RaiseOnWeaponAdded;
        _weaponInventory.OnAmmoAdded += RaiseOnAmmoAdded;
        _weaponInventory.OnWeaponDropped += RaiseOnWeaponDropped;
    }

    private void OnDisable()
    {
        _weaponInventory.OnWeaponAdded -= RaiseOnWeaponAdded;
        _weaponInventory.OnAmmoAdded -= RaiseOnAmmoAdded;
        _weaponInventory.OnWeaponDropped -= RaiseOnWeaponDropped;
    }



    public void AnimateAiming()
    {
        _animator.SetBool(_animIDAiming, true);
        _weaponAnimator.SetBool(_animIDAiming, true);
        _animator.SetLayerWeight(_animLayerIDAiming, 1);
    }

    public void AnimateNonAiming()
    {
        _animator.SetBool(_animIDAiming, false);
        _weaponAnimator.SetBool(_animIDAiming, false);
    }

    public virtual void SelectWeapon(int index)
    {
        _weaponInventory.SelectWeapon(index);

        if (CurrentWeapon != null)
        {
            _weaponAnimator.runtimeAnimatorController = _weaponInventory.CurrentWeapon.AnimationInfo.AnimatorOverrideController;

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

    public IEnumerator ShowActiveWeapon()
    {
        WeaponEquipping = true;
        CurrentWeapon.gameObject.SetActive(true);
        _weaponRig.weight = 1;
        _animator.SetLayerWeight(_animLayerIDAiming, 1);
        _weaponAnimator.Play("Weapon Equip");
        yield return new WaitForSeconds(_weaponAnimator.GetCurrentAnimatorStateInfo(0).length);
        WeaponEquipping = false;
    }

    public IEnumerator HideActiveWeapon()
    {
        _weaponAnimator.SetTrigger(_animIDHide);
        yield return new WaitForSeconds(_weaponAnimator.GetCurrentAnimatorStateInfo(0).length);
        CurrentWeapon.gameObject.SetActive(false);
        _weaponRig.weight = 0;
        _animator.SetLayerWeight(_animLayerIDAiming, 0);
    }

    public void DropActiveWeapon()
    {
        _weaponInventory.DropWeapon();
        _weaponRig.weight = 0;
        _animator.SetLayerWeight(_animLayerIDAiming, 0);
    }

    public virtual void Shoot(Vector3 direction)
    {
        CurrentWeapon.Shoot(direction);
        _weaponAnimator.SetTrigger("Shoot");

        OnShoot?.Invoke(
            new WeaponEventArgs
            {
                CurrentWeapon = CurrentWeapon,
                AmmoInMagazine = CurrentWeapon.Ammo,
                AmmoInInventory = _weaponInventory.CurrentAmmo.Count
            }
        );
    }

    public virtual IEnumerator Reload()
    {
        _weaponAnimator.SetTrigger("Reload");
        yield return new WaitForSeconds(0.1f); // Ожидание чтобы _weaponAnimator смог нормально переключиться на Weapon Reload
        CurrentWeapon.Reload(CurrentWeapon.ShootingInfo.MaxAmmo);
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

    private void RaiseOnWeaponAdded(WeaponEventArgs weaponEventArgs)
    {
        SelectWeapon(weaponEventArgs.SlotIndex);
        OnWeaponAdded?.Invoke(weaponEventArgs);
    }

    private void RaiseOnAmmoAdded(WeaponEventArgs weaponEventArgs)
    {
        OnAmmoAdded?.Invoke(weaponEventArgs);
    }

    private void RaiseOnWeaponDropped(WeaponEventArgs weaponEventArgs)
    {
        OnWeaponDropped?.Invoke(weaponEventArgs);
    }
}