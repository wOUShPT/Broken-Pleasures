using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class PlayerBehaviour : MonoBehaviour
{
    private InputHandler _inputHandler;
    public float grabDistance;
    private GameManager _gameManager;
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
    private FMOD.Studio.EventInstance _grab;
    private FMOD.Studio.EventInstance _throw;
    void Start()
    {
        hit = new RaycastHit();
        _isGrabbing = false;
        canGrab = false;
        _mainCamera = Camera.main;
        _inputHandler = FindObjectOfType<InputHandler>();
        _uiManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _grab = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Grab");
        _throw = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Throw");
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
                _uiManager.ToggleGrabPrompt(true);
            }
            else
            {
                canGrab = false;
                _uiManager.ToggleGrabPrompt(false);
            }
        }
        else
        {
            canGrab = false;
        }
    }

    private void Update()
    {
        if (_gameManager.currentGameState == GameManager.GameMode.GameLoop)
        {
            if (_inputHandler.MouseButton && _inputHandler != null)
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
                    _currentObjectRb.angularVelocity = Vector3.zero;
                    _uiManager.ToggleIdleReticle(false);
                    _uiManager.ToggleGrabPrompt(false);
                    _grab.start();
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
                    _uiManager.ToggleIdleReticle(true);
                    _uiManager.ToggleGrabPrompt(false);
                    _throw.start();
                }
                animator.SetBool("Pick", false);
            }
        }
    }
    
}