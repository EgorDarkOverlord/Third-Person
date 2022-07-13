using System;

public enum AmmoType
{
    Pistol,
    Rifle
}

[Serializable]
public class Ammo
{
    public AmmoType Type;
    public int Count;
}

