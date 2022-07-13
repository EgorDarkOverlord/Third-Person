using UnityEngine;

[CreateAssetMenu(fileName = "WeaponPickupInfo", menuName = "Weapon/WeaponPickupInfo")]
public class WeaponPickupInfo : ScriptableObject
{
    public WeaponPickup ArmedWeaponPrefab;
    public WeaponPickup UnarmedWeaponPrefab;
}

