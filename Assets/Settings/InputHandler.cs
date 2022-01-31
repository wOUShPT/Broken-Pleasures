using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Vector2 mousePosition;
    
    private bool mouseButton;

    public Vector2 MousePosition
    {
        get => mousePosition;
        set => mousePosition = value;
    }

    public bool MouseButton
    {
        get => mouseButton;
        set => mouseButton = value;
    }
    
    private void Awake()
    {
        mousePosition = Vector2.zero;
        mouseButton = false;
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mouseButton = Input.GetMouseButton(0);
        //Debug.Log("Mouse Debug: Position = " + mousePosition + " / isPressed = " + mouseButton);
    }
}
