using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Conveyor : MonoBehaviour
{
    public float speed;
    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;
    private float currentPositionOffSet;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.SetFloat("_Speed", speed);
        currentPositionOffSet = 0;
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
