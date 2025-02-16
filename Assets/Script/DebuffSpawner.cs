using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSpawner : MonoBehaviour 
{
    public GameObject[] debuffPrefabs;
    public int maxDebuffs = 3;
    public float spawnInterval = 5f;
    private List<GameObject> activeDebuff = new List<GameObject>();
    private void Start()
    {
        StartCoroutine(SpawnDebuffCorountine());
    }
    private IEnumerator SpawnDebuffCorountine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if(activeDebuff.Count < maxDebuffs)
            {
                SpawnDebuff();
            }
        }
    }
    private void SpawnDebuff()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4f,4f),5f,0);
        GameObject debuff = Instantiate(debuffPrefabs[Random.Range(0,debuffPrefabs.Length)],spawnPos,Quaternion.identity);
        activeDebuff.Add(debuff);

        StartCoroutine(DestroyAfterTime(debuff,10f));
    }
    private IEnumerator DestroyAfterTime(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if(obj != null)
        {
            activeDebuff.Remove(obj);
            Destroy(obj);
        }
    }
}