using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayData")]
public class DaySO : ScriptableObject
{
    private List<string> _days;

    private string _currentDay;
    
    private int _currentIndex;

    public int currentIndex => _currentIndex;

    public string currentDay => _days[_currentIndex];
    
    public DaySO()
    {
        _days = new List<string>();
        _days.Add("Monday");
        _days.Add("Tuesday");
        _days.Add("Wednesday");
        _days.Add("Thursday");
        _days.Add("Friday");
        _days.Add("Saturday");
        _days.Add("Sunday");

        _currentIndex = 0;
    }

    public void SkipDay()
    {
        if (_currentIndex == _days.Count - 1)
        {
            _currentIndex = 0;
            return;
        }

        _currentIndex++;
    }
}
