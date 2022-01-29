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

    private Vector3 spawnPosition
    {
        get => new Vector3(
            Random.Range(transform.position.x - (horizontalSize / 2), transform.position.x + (horizontalSize / 2)),
            transform.position.y, transform.position.z);
    }
    

    void Awake()
    {
        //InvokeRepeating("InstatiatePrefab", 0, 0.25f);
        _gameManager = FindObjectOfType<GameManager>();
        InitSpawn();
    }

    void InitSpawn()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (_gameManager.gameModeState == GameManager.GameMode.GameLoop)
        {
            pool.InstantiatePrefab(spawnPosition, Random.rotation);
            
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            yield return null;
        }
    }
}
