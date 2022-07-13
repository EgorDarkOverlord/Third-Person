using UnityEngine;

[CreateAssetMenu(fileName = "WeaponAnimationInfo", menuName = "Weapon/WeaponAnimationInfo")]
public class WeaponAnimationInfo : ScriptableObject
{
    public Vector3 LeftArmIkTargetPosition;
    public Vector3 LeftArmIkTargetRotation;
    public Vector3 RightArmIkTargetPosition;
    public Vector3 RightArmIkTargetRotation;
    public Vector3 WeaponHolderPosition;
    public Vector3 WeaponHolderRotation;
    public Vector3 WeaponHolderAimingPosition;
    public Vector3 WeaponHolderAimingRotation;

    public AnimatorOverrideController AnimatorOverrideController;
}