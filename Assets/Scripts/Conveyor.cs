using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Conveyor : MonoBehaviour
{
    public float speed;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        Vector3 position = _rb.position;
        _rb.position += Vector3.forward * speed * Time.fixedDeltaTime;
        _rb.MovePosition(position);
    }
}
