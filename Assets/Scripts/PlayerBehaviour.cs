using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private InputHandler _inputHandler;
    public float grabDistance;
    private UIManager _uiManager;
    public Transform grabPivot;
    public Animator animator;
    private RaycastHit hit;
    public Transform _poolTranform;
    private Rigidbody _lastObjectRB;
    private Rigidbody _currentObjectRb;
    private Collider _currentObjectCollider;
    private bool _isGrabbing;
    private bool canGrab;
    private Ray _ray;
    private Camera _mainCamera;
    void Start()
    {
        hit = new RaycastHit();
        _isGrabbing = false;
        canGrab = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        _mainCamera = Camera.main;
        _inputHandler = FindObjectOfType<InputHandler>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, grabDistance) && !_isGrabbing)
        {
            if (hit.collider.CompareTag("Object"))
            {
                _currentObjectRb = hit.rigidbody;
                _currentObjectCollider = hit.collider;
                canGrab = true;
            }
        }
        else
        {
            canGrab = false;
        }
    }

    private void Update()
    {
        if (_inputHandler.MouseButton)
        {
            if (canGrab)
            {
                _isGrabbing = true;
                canGrab = false;
                _currentObjectCollider.enabled = false;
                _currentObjectRb.transform.position = grabPivot.position;
                _currentObjectRb.transform.parent = grabPivot.transform;
                _currentObjectRb.useGravity = false;
                _currentObjectRb.velocity = Vector3.zero;
            }
            animator.SetBool("Pick", true);
        }
        else
        {
            if (_isGrabbing)
            {
                _isGrabbing = false;
                _currentObjectRb.transform.parent = _poolTranform;
                _currentObjectCollider.enabled = true;
                _currentObjectRb.useGravity = true;
                _currentObjectRb.AddForce(_mainCamera.transform.forward * 5f, ForceMode.Impulse);
                _currentObjectRb = null;
                _currentObjectCollider = null;
            }
            animator.SetBool("Pick", false);
        }
    }
    
}