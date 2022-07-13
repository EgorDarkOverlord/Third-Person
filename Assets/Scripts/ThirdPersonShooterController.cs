using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField] private float _idleSensivity;
    [SerializeField] private float _aimSensivity;
    [SerializeField] private LayerMask _aimColliderLayerMask;
    [SerializeField] private Transform _debugTransform;

    private ThirdPersonController _thirdPersonController;
    private Animator _animator;
    private Animator _weaponAnimator;
    private StarterAssetsInputs _inputs;
    private Camera _mainCamera;
    private Rig _weaponRig;
    private CinemachineImpulseSource _cameraShake;
    [SerializeField] private Weapon _activeWeapon;
    [SerializeField] private Weapon[] _weaponSlots;
    private WeaponInventory _weaponInventory;
    private bool _weaponEquipped = true;

    private int _animIDAiming;
    private int _animIDHorizontalDirection;
    private int _animIDVerticalDirection;
    private int _animLayerIDAiming;


    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();
        _weaponRig = transform.Find("Weapon Rig").GetComponent<Rig>();
        _cameraShake = GetComponent<CinemachineImpulseSource>();
        _weaponAnimator = _weaponRig.GetComponent<Animator>();
        _weaponInventory = GetComponent<WeaponInventory>();
        _inputs = GetComponent<StarterAssetsInputs>();
        _mainCamera = Camera.main;

        _weaponInventory.SelectWeapon(0);

        _animIDAiming = Animator.StringToHash("Aiming");
        _animIDHorizontalDirection = Animator.StringToHash("HorizontalDirection");
        _animIDVerticalDirection = Animator.StringToHash("VerticalDirection");
        _animLayerIDAiming = _animator.GetLayerIndex("Aiming");
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = FindMouseWorldPosition();

        if (_inputs.aim)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.Sensivity = _aimSensivity;

            AnimateAiming();

            RotateBodyFromAim(mouseWorldPosition);
        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.Sensivity = _idleSensivity;

            AnimateNonAiming();

            CheckHideOrShowWeapon();
            CheckSwitchingWeapon();
        }

        CheckShoot(mouseWorldPosition);
        CheckReload();
    }
    


    private Vector3 FindMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = _mainCamera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderLayerMask))
        {
            _debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        return mouseWorldPosition;
    }

    private void RotateBodyFromAim(Vector3 mouseWorldPosition)
    {
        Vector3 worldAimPosition = mouseWorldPosition;
        worldAimPosition.y = transform.position.y;
        Vector3 aimDirection = (worldAimPosition - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
    }

    private void AnimateNonAiming()
    {
        _animator.SetBool(_animIDAiming, false);
        _weaponAnimator.SetBool("Aiming", false);
        //_animator.SetLayerWeight(_animLayerIDAiming, 0);
        _animator.SetFloat(_animIDHorizontalDirection, 0);
        _animator.SetFloat(_animIDVerticalDirection, 0);
    }

    private void AnimateAiming()
    {
        _animator.SetBool(_animIDAiming, true);
        _weaponAnimator.SetBool("Aiming", true);
        _animator.SetLayerWeight(_animLayerIDAiming, 1);
        _animator.SetFloat(_animIDHorizontalDirection, _inputs.move.x);
        _animator.SetFloat(_animIDVerticalDirection, _inputs.move.y);
    }

    private void CheckHideOrShowWeapon()
    {
        if (_inputs.hideOrShowWeapon)
        {
            if (_weaponEquipped)
            {
                StartCoroutine(HideActiveWeapon());
                _weaponEquipped = false;
            }
            else
            {
                StartCoroutine(ShowActiveWeapon());
                _weaponEquipped = true;
            }
            _inputs.hideOrShowWeapon = false;
        }
    }

    private void CheckSwitchingWeapon()
    {
        if (_inputs.selectedWeaponNumber != 0)
        {
            if (_weaponEquipped)
            {
                StartCoroutine(SwitchWeapon(_inputs.selectedWeaponNumber - 1));
            }
            else
            {
                //_activeWeapon = _weaponSlots[_inputs.selectedWeaponNumber - 1];
                //_weaponAnimator.runtimeAnimatorController = _activeWeapon.AnimationInfo.AnimatorOverrideController;
                //_cameraShake.m_ImpulseDefinition.m_RawSignal = _activeWeapon.ShootingInfo.NoiseSettings;
                _weaponInventory.SelectWeapon(_inputs.selectedWeaponNumber - 1);
                if (_weaponInventory.CurrentWeapon != null)
                {
                    _weaponAnimator.runtimeAnimatorController = _weaponInventory.CurrentWeapon.AnimationInfo.AnimatorOverrideController;
                    _cameraShake.m_ImpulseDefinition.m_RawSignal = _weaponInventory.CurrentWeapon.ShootingInfo.NoiseSettings;
                }

            }
            _inputs.selectedWeaponNumber = 0;
        }
    }

    private void CheckShoot(Vector3 mouseWorldPosition)
    {
        if (_inputs.shoot && _activeWeapon.CanShoot())
        {
            Vector3 aimDirection = (mouseWorldPosition - _activeWeapon.SpawnBulletTransform.position).normalized;
            //_activeWeapon.Shoot(aimDirection);
            _weaponInventory.CurrentWeapon.Shoot(aimDirection);
            _cameraShake.GenerateImpulse();
            _weaponAnimator.SetTrigger("Shoot");
        }
    }

    private void CheckReload()
    {
        if (_inputs.reload)
        {
            _inputs.reload = false;
            _inputs.aim = false;
            StartCoroutine(Reload());
        }
    }


    private IEnumerator HideActiveWeapon()
    {
        _weaponAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(_weaponAnimator.GetCurrentAnimatorStateInfo(0).length);
        //_activeWeapon.gameObject.SetActive(false);
        _weaponInventory.CurrentWeapon.gameObject.SetActive(false);
        _weaponRig.weight = 0;
        _animator.SetLayerWeight(_animLayerIDAiming, 0);
    }

    private IEnumerator ShowActiveWeapon()
    {
        //_activeWeapon.gameObject.SetActive(true);
        _weaponInventory.CurrentWeapon.gameObject.SetActive(true);
        _weaponRig.weight = 1;
        _animator.SetLayerWeight(_animLayerIDAiming, 1);
        _weaponAnimator.Play("Weapon Equip");
        yield return new WaitForSeconds(_weaponAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    private IEnumerator SwitchWeapon(int selectedWeaponNumber)
    {
        yield return StartCoroutine(HideActiveWeapon());
        //_activeWeapon = _weaponSlots[selectedWeaponNumber];
        //_weaponAnimator.runtimeAnimatorController = _activeWeapon.AnimationInfo.AnimatorOverrideController;
        //_cameraShake.m_ImpulseDefinition.m_RawSignal = _activeWeapon.ShootingInfo.NoiseSettings;
        _weaponInventory.SelectWeapon(selectedWeaponNumber);
        if (_weaponInventory.CurrentWeapon != null)
        {
            _weaponAnimator.runtimeAnimatorController = _weaponInventory.CurrentWeapon.AnimationInfo.AnimatorOverrideController;
            _cameraShake.m_ImpulseDefinition.m_RawSignal = _weaponInventory.CurrentWeapon.ShootingInfo.NoiseSettings;
        }
        yield return StartCoroutine(ShowActiveWeapon());
    }

    private IEnumerator Reload()
    {
        if (_weaponAnimator.GetBool("Aiming"))
        {
            _weaponAnimator.SetBool("Aiming", false);
            yield return new WaitForSeconds(_weaponAnimator.GetCurrentAnimatorStateInfo(0).length);
        }
        _weaponAnimator.SetTrigger("Reload");
        _activeWeapon.Reload();
        yield return new WaitForSeconds(_weaponAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}
