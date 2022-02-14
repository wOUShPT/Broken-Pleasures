using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayData")]
public class DaySO : ScriptableObject
{
    public int currentIndex;
    
    public void SkipDay()
    {
        if (currentIndex == 6)
        {
            currentIndex = 0;
            Debug.Log(currentIndex);
            return;
        }

        currentIndex++;
        Debug.Log(currentIndex);
    }
}
