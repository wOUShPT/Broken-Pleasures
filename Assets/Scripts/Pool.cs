using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pool : MonoBehaviour
{
    public int poolSize;
    public GameObject prefab;
    public List<GameObject> objectList;
    private List<GameObject> activeObjects;
    private Queue<GameObject> _unusedObjects;

    private void Awake()
    {
        
    }

    public void Init()
    {
        objectList = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject currentObject = InstantiatePrefab(prefab, transform.position, Random.rotation);
            objectList.Add(currentObject);
            SendToPool(currentObject);
        }
    }

    public GameObject GetFromPool()
    {
        GameObject currentObject = _unusedObjects.Dequeue();
        currentObject.SetActive(true);
        return _unusedObjects.Dequeue();
    }

    public void SendToPool(GameObject currentObject)
    {
        currentObject.SetActive(false);
        _unusedObjects.Enqueue(currentObject);
    }


    public GameObject InstantiatePrefab(GameObject currentPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject currentObject = Instantiate(currentPrefab, position, rotation, transform);
        objectList.Add(currentObject);
        return currentObject;
    }
}
