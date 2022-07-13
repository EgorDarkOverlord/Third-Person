using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnInfo SpawnInfo;
    public Transform SpawnTransform;
    public float SpawnTime;
    public LayerMask BarrierMask;

    private void Start()
    {
        StartCoroutine(SpawnProcess());
    }

    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnTime);
            
            while (Physics.CheckSphere(SpawnTransform.position, 0.5f, BarrierMask))
            {
                yield return new WaitForSeconds(0.5f);
            }

            Spawn();        
        }
    }

    private void Spawn()
    {
        int factor = 0;
        int select = 0;
        int maxSelect = 0;

        for (int i = 0; i < SpawnInfo.SpawnItems.Length; i++)
        {
            maxSelect += SpawnInfo.SpawnItems[i].SpawnFactor;
        }
        select = Random.Range(0, maxSelect);

        for (int i = 0; i < SpawnInfo.SpawnItems.Length; i++)
        {
            factor += SpawnInfo.SpawnItems[i].SpawnFactor;
            if (factor > select)
            {
                Instantiate(SpawnInfo.SpawnItems[i].SpawnObject, SpawnTransform.position, Quaternion.identity);
                return;
            }
        }
    }
}