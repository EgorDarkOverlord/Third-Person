using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FightingInfo", menuName = "Fighting/FightingInfo")]
public class FightingInfo : ScriptableObject
{
    [SerializeField] private HitInfo[] _hits;

    public HitInfo FindByIndex(int fightIndex)
    {
        for (int i = 0; i < _hits.Length; i++)
        {
            if (_hits[i].FightIndex == fightIndex)
            {
                return _hits[i];
            }
        }

        return null;
    }
}

[Serializable]
public class HitInfo
{
    public int FightIndex;
    public float Damage;
    public float Delay;
    public Hurtbox Hurtbox;

    public void Enable()
    {
        Hurtbox.enabled = true;
        Hurtbox.Damage = Damage;
    }

    public void Disable()
    {
        Hurtbox.enabled = false;
        Hurtbox.Damage = 0;
    }
}