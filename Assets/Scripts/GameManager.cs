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
    public float gameDuration;
    public float score;
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
        score = 0;
        currentGameState = GameMode.Idle;
        StartCoroutine(StartRound());
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
                StartCoroutine(EndRound());
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

    IEnumerator StartRound()
    {
        _uiManager.SetDay(GameData.Day.Index);
        Debug.Log(GameData.Day.Index);
        Debug.Log(GameData.PowerUp);
        _uiManager.ToggleIdleReticle(false);
        _uiManager.ToggleGrabPrompt(false);
        _uiManager.ToggleCoinInfo(false);
        ToggleMouseCursor(false);
        ToggleCamMotion(false);
        _uiManager.ToggleTimer(false);
        _uiManager.ToggleScoreLeaderboard(false);
        _uiManager.ToggleIntroOutroText(false);
        if (GameData.PowerUp != GameData.PowerUpType.None)
        {
            yield return new WaitForSeconds(3f);
            _uiManager.SetPowerUp((int)GameData.PowerUp);
        }
        yield return new WaitForSeconds(5);
        _uiManager.SetCoin(GameData.Coins.Amount);
        _uiManager.ToggleCoinInfo(true);
        _uiManager.RunCounterToInit();
        yield return new WaitForSeconds(3f);
        currentGameState = GameMode.GameLoop;
        timer = gameDuration;
        _uiManager.SetTimer(timer);
        _uiManager.ToggleTimer(true);
        ToggleCamMotion(true);
        _horn.start();
        yield return new WaitForSeconds(1f);
        _music.start();
        _spawner.InitSpawn();
        yield return new WaitForSeconds(0.3f);
        _uiManager.ToggleIdleReticle(true);
        yield return null;
    }

    IEnumerator EndRound()
    {
        _uiManager.ToggleIdleReticle(false);
        _uiManager.ToggleGrabPrompt(false);
        _music.stop(STOP_MODE.ALLOWFADEOUT);
        _horn.start();
        _uiManager.ToggleTimer(false);
        _spawner.StopSpawn();
        _uiManager.RunOutroText();
        ToggleCamMotion(false);
        yield return new WaitForSeconds(3f);
        _uiManager.SetScoreOutro(score);
        GameData.Day.SkipDay();
        GameData.Coins.IncreaseMoney(CalculateCoinGain());
        _uiManager.SetCoin(GameData.Coins.Amount);
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
        _uiManager.scoreLeaderboard.interactable = false;
        Application.Quit();
    }


    IEnumerator TransitionToNextDayCoroutine()
    {
        _uiManager.scoreLeaderboard.interactable = false;
        sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        GameData.PowerUp = GameData.PowerUpType.None;
        _ambient.stop(STOP_MODE.ALLOWFADEOUT);
        _ambient02.stop(STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Game");
    }
    
    IEnumerator TransitionToShopCoroutine()
    {
        _uiManager.scoreLeaderboard.interactable = false;
        sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        GameData.PowerUp = GameData.PowerUpType.None;
        _ambient.stop(STOP_MODE.ALLOWFADEOUT);
        _ambient02.stop(STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Shop");
    }

    public int CalculateCoinGain()
    {
        return (int)(score / 2);
    }
}
