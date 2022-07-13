using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon Weapon;
    public Ammo Ammo;
    
    [Space]
    [SerializeField] private float _rotationSpeed = 90f;
    [SerializeField] private float _lifeTime;
    private float _spawnTime;



    private void Start()
    {
        _spawnTime = Time.time;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, _rotationSpeed, 0)  * Time.deltaTime);

        if (Time.time > _spawnTime + _lifeTime)
        {
            Destroy(gameObject);
        }
    }



    public void PickupAmmo(WeaponInventory weaponInventory)
    {
        weaponInventory.AddAmmo(Ammo);
        Instantiate(Weapon.PickupInfo.UnarmedWeaponPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void Pickup(WeaponInventory weaponInventory)
    {
        if (weaponInventory.TryAddWeapon(Instantiate(Weapon)))
        {
            Destroy(gameObject);
        }
    }
}
