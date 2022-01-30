using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Box : MonoBehaviour
{
    private GameManager _gameManager;
    public string type;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            if (other.GetComponent<ObjectType>().type == type)
            {
                _gameManager.Score(2);
            }
            else
            {
                _gameManager.Score(-2);
            }

        }
    }
}
