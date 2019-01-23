using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public float timeOfLastSpawn, timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new GameObject[transform.childCount];
        PopulateArray();
    }

    void PopulateArray()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeOfLastSpawn >= timeBetweenSpawns)
        {
            Instantiate(enemyPrefab, spawnPoints[Random.Range(0, transform.childCount)].transform.position , spawnPoints[Random.Range(0, transform.childCount)].transform.rotation);
            timeOfLastSpawn = Time.time;
        }
    }
}
