using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineAnimation : MonoBehaviour
{
    private GameManager _gameManager;
    public Animator lightAnimator;
    private bool _isOn;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _isOn = false;
    }

    void Update()
    {
        _isOn = _gameManager.currentGameState == GameManager.GameMode.GameLoop;
        lightAnimator.SetBool("isOn", _isOn);
    }
}
