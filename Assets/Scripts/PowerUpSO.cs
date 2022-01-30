using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpData")]
public class PowerUpSO : ScriptableObject
{
    public PowerUp currentPowerUp;

    [Serializable]
    public enum PowerUp
    {
        None, Powder, Jav, Magnet
    }

    public PowerUpSO()
    {
        currentPowerUp = PowerUp.None;
    }
}
