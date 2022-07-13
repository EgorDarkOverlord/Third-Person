using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnInfo", menuName = "Spawn/SpawnInfo")]
public class SpawnInfo : ScriptableObject
{
    public SpawnItem[] SpawnItems;
}

[Serializable]
public class SpawnItem
{
    public GameObject SpawnObject;
    public int SpawnFactor;
}