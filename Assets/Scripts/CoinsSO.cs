using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CoinsData")]
public class CoinsSO : ScriptableObject
{
    public int _amount = 0;

    public int amount
    {
        get => _amount;
    }

    public void IncreaseMoney(int increment)
    {
        _amount += increment;
    }

    public void DecreaseMoney(int decrement)
    {
        _amount -= decrement;
        _amount = Mathf.Clamp(amount, 0, Int32.MaxValue);
    }
}
