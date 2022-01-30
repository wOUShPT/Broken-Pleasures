using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public int score;
    public PowerUpSO powerUpData;
    public CoinsSO coinData;
    public DaySO daysData;
    public float timer;
    private Spawner _spawner;
    private UIManager _uiManager;
    private CinemachineVirtualCamera _vCam;
    public Animator sceneTransitionAnimator;

    public enum GameMode
    {
        GameLoop, Shop, Idle
    }

    public GameMode currentGameState;


    private void Awake()
    {
        _vCam = FindObjectOfType<CinemachineVirtualCamera>();
        _uiManager = FindObjectOfType<UIManager>();
        _spawner = FindObjectOfType<Spawner>();
        currentGameState = GameMode.GameLoop;
        score = 0;
        currentGameState = GameMode.Idle;
        StartCoroutine(Intro());
    }

    private void Update()
    {
        if (currentGameState == GameMode.GameLoop)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, 61);
            if (timer <= 0)
            {
                currentGameState = GameMode.Idle;
                StartCoroutine(Outro());
            }
            _uiManager.SetTimer(timer);
        }
    }

    public void Score(int value)
    {
        if (currentGameState == GameMode.GameLoop)
        {
            score += value;
        }
        score = Mathf.Clamp(score, 0, Int32.MaxValue);
    }

    IEnumerator Intro()
    {
        _uiManager.ToggleCoinInfo(false);
        ToggleMouseCursor(false);
        ToggleCamMotion(false);
        _uiManager.SetDay(daysData.currentIndex);
        _uiManager.ToggleTimer(false);
        _uiManager.ToggleIdleReticle(false);
        _uiManager.ToggleScoreLeaderboard(false);
        _uiManager.ToggleIntroOutroText(false);
        yield return new WaitForSeconds(5);
        _uiManager.SetCoin(coinData.amount);
        _uiManager.ToggleCoinInfo(true);
        _uiManager.RunCounterToInit();
        yield return new WaitForSeconds(3f);
        currentGameState = GameMode.GameLoop;
        timer = 60;
        _uiManager.SetTimer(timer);
        _uiManager.ToggleTimer(true);
        _uiManager.ToggleIdleReticle(true);
        ToggleCamMotion(true);
        _spawner.InitSpawn();
        yield return null;
    }

    IEnumerator Outro()
    {
        _uiManager.ToggleTimer(false);
        _spawner.StopSpawn();
        _uiManager.RunOutroText();
        yield return new WaitForSeconds(3f);
        _uiManager.SetScoreOutro(score);
        coinData.IncreaseMoney(CalculateCoinGain());
        _uiManager.SetCoin(coinData.amount);
        _uiManager.ToggleScoreLeaderboard(true);
        ToggleCamMotion(false);
        ToggleMouseCursor(true);
        yield return new WaitForSeconds(8f);
        yield return null;
    }

    public void GoToNextDay()
    {
        StartCoroutine(TransitionToNextDayCoroutine());
    }


    public void GoToShop()
    {
        StartCoroutine(TransitionToShopCoroutine());
    }

    void ToggleCamMotion(bool state)
    {
        _vCam.enabled = state;
    }

    void ToggleMouseCursor(bool state)
    {
        if (state)
        {
            Cursor.SetCursor(_uiManager.cursorImage, Vector2.zero, CursorMode.Auto);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }


    IEnumerator TransitionToNextDayCoroutine()
    {
        sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        daysData.SkipDay();
        powerUpData.currentPowerUp = PowerUpSO.PowerUp.None;
        SceneManager.LoadScene("Game");
    }
    
    IEnumerator TransitionToShopCoroutine()
    {
        sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        daysData.SkipDay();
        powerUpData.currentPowerUp = PowerUpSO.PowerUp.None;
        SceneManager.LoadScene("Shop");
    }

    public int CalculateCoinGain()
    {
        return score / 2;
    }
}
