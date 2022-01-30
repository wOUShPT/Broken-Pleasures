using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    public CanvasGroup idleReticle;
    public CanvasGroup timer;
    private TextMeshProUGUI _timerText;
    public CanvasGroup scoreLeaderboard;
    public CanvasGroup introOutroInfo;
    public TextMeshProUGUI scoreOutroText;
    private TextMeshProUGUI introOutroInfoText;
    public Image day;
    public List<Sprite> daysSprites;
    public Texture2D cursorImage;
    public CanvasGroup coinInfo;
    private TextMeshProUGUI _coinText;
    //public CanvasGroup grabReticle;

    private void Awake()
    {
        introOutroInfoText = introOutroInfo.GetComponent<TextMeshProUGUI>();
        _timerText = timer.GetComponentInChildren<TextMeshProUGUI>();
        _coinText = coinInfo.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ToggleIdleReticle(bool state)
    {
        idleReticle.alpha = state ? 1 : 0;
    }
    
    public void ToggleGrabReticle(bool state)
    {
        idleReticle.alpha = state ? 1 : 0;
    }


    public void ToggleScoreLeaderboard(bool state)
    {
        scoreLeaderboard.alpha = state ? 1 : 0;
    }

    public void RunCounterToInit()
    {
        StartCoroutine(CounterToInitCoroutine());
    }

    public void ToggleTimer(bool state)
    {
       timer.alpha = state ? 1 : 0;
    }

    public void ToggleIntroOutroText(bool state)
    {
        introOutroInfo.alpha = state ? 1 : 0;
    }
    
    public void ToggleCoinInfo(bool state)
    {
        coinInfo.alpha = state ? 1 : 0;
    }

    public void SetTimer(float value)
    {
        _timerText.text = Mathf.Ceil(value).ToString();
    }
    

    IEnumerator CounterToInitCoroutine()
    {
        ToggleIntroOutroText(true);
        introOutroInfoText.text = 3.ToString();
        yield return new WaitForSeconds(1f);
        introOutroInfoText.text = 2.ToString();
        yield return new WaitForSeconds(1f);
        introOutroInfoText.text = 1.ToString();
        yield return new WaitForSeconds(1f);
        introOutroInfoText.text = "Start";
        yield return new WaitForSeconds(1f);
        ToggleIntroOutroText(false);
    }

    public void RunOutroText()
    {
        StartCoroutine(OutroTextCoroutine());
    }
    
    
    IEnumerator OutroTextCoroutine()
    {
        introOutroInfoText.text = "Time's up!";
        ToggleIntroOutroText(true);
        yield return new WaitForSeconds(3f);
        ToggleIntroOutroText(false);
    }

    public void SetScoreOutro(float value)
    {
        scoreOutroText.text = value.ToString();
    }

    public void SetDay(int index)
    {
        day.sprite = daysSprites[index];
    }

    public void SetCoin(int value)
    {
        _coinText.text = value.ToString();
    }
}