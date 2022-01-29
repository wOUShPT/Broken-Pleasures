using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public int score;
    public int coinsNumber;
    public float timer;

    public enum GameMode
    {
        GameLoop, Shop, Idle
    }

    public GameMode gameModeState;


    private void Awake()
    {
        gameModeState = GameMode.GameLoop;
    }

    public void Score(int value)
    {
        score += value;
        Mathf.Clamp(score, 0, Int32.MaxValue);
    }
}
