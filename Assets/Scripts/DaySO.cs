using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayData")]
public class DaySO : ScriptableObject
{
    public int currentIndex = 0;
    
    public void SkipDay()
    {
        if (currentIndex == 7)
        {
            currentIndex = 0;
            return;
        }

        currentIndex++;
    }
}
