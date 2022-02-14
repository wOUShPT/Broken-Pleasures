using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float horizontalSize;
    public Pool pool;
    public GameObject _prefab;
    private GameManager _gameManager;
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public List<GameObject> prefabs;
    public List<float> spawnRateList;

    private Vector3 spawnPosition
    {
        get => new Vector3(
            Random.Range(transform.position.x - (horizontalSize / 2), transform.position.x + (horizontalSize / 2)),
            transform.position.y, transform.position.z);
    }
    

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (GameData.PowerUp == GameData.PowerUpType.Powder)
        {
            minSpawnInterval = 2;
            maxSpawnInterval = 2;
        }
    }

    public void InitSpawn()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (_gameManager.currentGameState == GameManager.GameMode.GameLoop)
        {
            pool.InstantiatePrefab(prefabs[GetWeightedRandomIndex()], spawnPosition, Random.rotation);
            
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            yield return null;
        }
    }

    public void StopSpawn()
    {
        StopCoroutine(Spawn());
    }
    
    private int GetWeightedRandomIndex()
    {
        float sum = 0;
        for (int i = 0; i < spawnRateList.Count; i++)
        {
            sum += spawnRateList[i];
        }
        float randomWeight = 0;
        
        do
        {
            if (sum == 0)
            {
                return 0;
            }
            
            randomWeight = Random.Range(0, sum);
        } 
        while (randomWeight == sum);
            
        for(int i = 0; i < spawnRateList.Count; i++)
        {
            if (randomWeight < spawnRateList[i])
            {
                    return i;
            }
            randomWeight -= spawnRateList[i];
        }

        return 0;
    }
}
