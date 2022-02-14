using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Conveyor : MonoBehaviour
{
    public float speed;
    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;
    private float currentPositionOffSet;
    private GameManager _gameManager;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.SetFloat("_Speed", speed);
        currentPositionOffSet = 0;
        _gameManager = FindObjectOfType<GameManager>();
        if (GameData.PowerUp == GameData.PowerUpType.Powder)
        {
            speed = speed / 1.2f;
        }
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 position = _rb.position;
        _rb.position += Vector3.forward * speed * Time.fixedDeltaTime;
        _rb.MovePosition(position);

        currentPositionOffSet += (speed * Time.fixedDeltaTime)/6;
        _meshRenderer.material.SetFloat("_currentPosition", currentPositionOffSet);
    }
}
