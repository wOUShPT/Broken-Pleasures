using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.XR;
using FMOD.Studio;

public class GameManager : MonoBehaviour
{
    public float score;
    public PowerUpSO powerUpData;
    public CoinsSO coinData;
    public DaySO daysData;
    public float timer;
    private Spawner _spawner;
    private UIManager _uiManager;
    private CinemachineVirtualCamera _vCam;
    public Animator sceneTransitionAnimator;
    private FMOD.Studio.EventInstance _music;
    private FMOD.Studio.EventInstance _ambient;
    private FMOD.Studio.EventInstance _ambient02;
    private FMOD.Studio.EventInstance _horn;
    

    public enum GameMode
    {
        GameLoop, Shop, Idle
    }

    public GameMode currentGameState;


    private void Awake()
    {
        _music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Theme");
        _ambient = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Factory");
        _ambient02 = FMODUnity.RuntimeManager.CreateInstance("event:/Ambient/Treadmill");
        _horn = FMODUnity.RuntimeManager.CreateInstance("event:/Stingers/FinalHorn");
        _ambient.start();
        _ambient.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        _ambient02.start();
        _ambient02.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        _ambient.setVolume(0.5f);
        _ambient02.setVolume(0.25f);
        _music.setVolume(0.7f);
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
        _horn.start();
        yield return new WaitForSeconds(1f);
        _music.start();
        _spawner.InitSpawn();
        yield return null;
    }

    IEnumerator Outro()
    {
        _music.stop(STOP_MODE.ALLOWFADEOUT);
        _horn.start();
        _uiManager.ToggleTimer(false);
        _spawner.StopSpawn();
        _uiManager.RunOutroText();
        ToggleCamMotion(false);
        yield return new WaitForSeconds(3f);
        _uiManager.SetScoreOutro(score);
        coinData.IncreaseMoney(CalculateCoinGain());
        _uiManager.SetCoin(coinData.amount);
        _uiManager.ToggleScoreLeaderboard(true);
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
        _ambient.stop(STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Game");
    }
    
    IEnumerator TransitionToShopCoroutine()
    {
        sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        daysData.SkipDay();
        powerUpData.currentPowerUp = PowerUpSO.PowerUp.None;
        _ambient.stop(STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Shop");
    }

    public int CalculateCoinGain()
    {
        return (int)(score / 2);
    }
}
