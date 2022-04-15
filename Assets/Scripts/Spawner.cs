using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public float horizontalSize;
    public Pool pool;
    private GameManager _gameManager;
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public List<SexToy> sexToys;

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
            pool.InstantiatePrefab(sexToys[GetWeightedRandomIndex()].prefab, spawnPosition, Random.rotation);
            
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
        for (int i = 0; i < sexToys.Count; i++)
        {
            sum += sexToys[i].weight;
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
            
        for(int i = 0; i < sexToys.Count; i++)
        {
            if (randomWeight < sexToys[i].weight)
            {
                    return i;
            }
            randomWeight -= sexToys[i].weight;
        }

        return 0;
    }
}
