using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject mushroomPrefab;
    public GameObject[] powerUpPrefabs;
    public int poolSize = 10;
    private List<GameObject> mushroomPool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject mushroom = Instantiate(mushroomPrefab);
            mushroom.SetActive(false);
            mushroomPool.Add(mushroom);
        }
        InvokeRepeating("SpawnMushroom", 1f, 2f); // Gọi liên tục sau mỗi 2s
        InvokeRepeating("SpawnPowerUp",5f,10f);
    }

    private void SpawnMushroom()
    {
        GameObject mushroom = GetPooledMushroom();
        if (mushroom != null)
        {
            mushroom.transform.position = new Vector3(Random.Range(-7f, 7f), 5f, 0);
            mushroom.SetActive(true);
        }
    }

    private void SpawnPowerUp()
    {
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject powerUp = Instantiate(powerUpPrefabs[randomIndex]);
        powerUp.transform.position = new Vector3(Random.Range(-7f,7f),5f,0);
    }

    private GameObject GetPooledMushroom()
    {
        foreach (GameObject mushroom in mushroomPool)
        {
            if (!mushroom.activeInHierarchy) return mushroom;
        }
        return null;
    }
}
