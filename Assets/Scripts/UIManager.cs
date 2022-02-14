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
    public CanvasGroup grabPrompt;
    public CanvasGroup timer;
    private TextMeshProUGUI _timerText;
    public CanvasGroup scoreLeaderboard;
    public CanvasGroup introOutroInfo;
    public TextMeshProUGUI scoreOutroText;
    private TextMeshProUGUI introOutroInfoText;
    public Image day;
    private Animator _dayAnimator;
    public List<Sprite> daysSprites;
    public Image powerUp;
    private Animator _powerUpAnimator;
    public List<Sprite> powerUps;
    public Texture2D cursorImage;
    public CanvasGroup coinInfo;
    private TextMeshProUGUI _coinText;
    //public CanvasGroup grabReticle;

    private void Awake()
    {
        introOutroInfoText = introOutroInfo.GetComponent<TextMeshProUGUI>();
        _timerText = timer.GetComponentInChildren<TextMeshProUGUI>();
        _coinText = coinInfo.GetComponentInChildren<TextMeshProUGUI>();
        _dayAnimator = day.GetComponent<Animator>();
        _powerUpAnimator = powerUp.GetComponent<Animator>();
    }

    public void ToggleIdleReticle(bool state)
    {
        idleReticle.alpha = state ? 1 : 0;
    }
    
    public void ToggleGrabPrompt(bool state)
    {
        grabPrompt.alpha = state ? 1 : 0;
    }


    public void ToggleScoreLeaderboard(bool state)
    {
        scoreLeaderboard.alpha = state ? 1 : 0;
        scoreLeaderboard.interactable = state;
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
        _dayAnimator.Play("Intro");
    }

    public void SetPowerUp(int index)
    {
        if (index == 0)
        {
            return;
        }
        powerUp.sprite = powerUps[index - 1];
        _powerUpAnimator.Play("Intro");
    }

    public void SetCoin(int value)
    {
        _coinText.text = value.ToString();
    }
}