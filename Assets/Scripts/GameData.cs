using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static DayClass Day = new DayClass();

    public static  CoinClass Coins = new CoinClass();

    public static PowerUpType PowerUp = PowerUpType.None;
    
    [Serializable]
    public enum PowerUpType
    {
        None = 0, Powder = 1, Jav = 2, Magnet = 3
    }

}

public class DayClass
{
    private int _currentIndex = 0;

    public int Index => _currentIndex;
        
    public void SkipDay()
    {
        if (_currentIndex == 6)
        {
            _currentIndex = 0;
            Debug.Log(_currentIndex);
            return;
        }

        _currentIndex++;
        Debug.Log(_currentIndex);
    }
}

public class CoinClass
{
    private int _amount = 0;

    public int Amount => _amount;

    public void IncreaseMoney(int increment)
    {
        _amount += increment;
    }

    public void DecreaseMoney(int decrement)
    {
        _amount -= decrement;
        _amount = Mathf.Clamp(_amount, 0, Int32.MaxValue);
    }
}
