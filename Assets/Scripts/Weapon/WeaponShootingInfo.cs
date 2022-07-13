using UnityEngine;
using Cinemachine;

[CreateAssetMenu(fileName = "WeaponShootingInfo", menuName = "Weapon/WeaponShootingInfo")]
public class WeaponShootingInfo : ScriptableObject
{
    public float Damage;
    public float FireRateInSeconds;
    public AmmoType RequiredAmmoType;
    public int MaxAmmo;
    public Bullet BulletProjectile;
    public AudioClip ShootingSound;
    public AudioClip ReloadSound;
    public NoiseSettings NoiseSettings;
}