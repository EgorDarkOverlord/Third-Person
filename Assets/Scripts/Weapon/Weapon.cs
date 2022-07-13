using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponShootingInfo ShootingInfo;
    public WeaponAnimationInfo AnimationInfo;
    public WeaponPickupInfo PickupInfo;
    public int Ammo;
    public string Name;
    
    private Transform _spawnBulletTransform;
    [SerializeField] private ParticleSystem _shootingEffect;
    private AudioSource _audioSource;

    public Transform SpawnBulletTransform { get => _spawnBulletTransform; }

    private float _shootTime;

    private void Start()
    {
        _spawnBulletTransform = gameObject.transform.Find("SpawnBulletTransform");
        _audioSource = GetComponent<AudioSource>();
    }

    public bool CanShoot()
    {
        return (Time.time > _shootTime + 1 / ShootingInfo.FireRateInSeconds) && (Ammo > 0);
    }
    
    public void Shoot(Vector3 aimDirection)
    {
        var bullet = Instantiate(ShootingInfo.BulletProjectile, _spawnBulletTransform.position, Quaternion.LookRotation(aimDirection, Vector3.up));
        bullet.Damage = ShootingInfo.Damage;
        Ammo--;
        _shootingEffect.Play(true);
        _audioSource.PlayOneShot(ShootingInfo.ShootingSound);
        _shootTime = Time.time;
    }

    public void Reload()
    {
        _audioSource.PlayOneShot(ShootingInfo.ReloadSound);
    }

    public void Reload(int ammoCount)
    {
        _audioSource.PlayOneShot(ShootingInfo.ReloadSound);
        Ammo += ammoCount;
    }
}

