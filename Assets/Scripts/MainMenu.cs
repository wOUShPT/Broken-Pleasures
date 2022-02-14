using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public CanvasGroup mainMenu;

    public CanvasGroup credits;

    public Animator _sceneTransitionAnimator;

    public Texture2D cursorImage;

    public MouseSense mouseSenseData;

    public Slider mouseSenseSlider;

    public DaySO daysData;

    public CoinsSO CoinsData;
    
    private FMOD.Studio.EventInstance _music;


    private void Awake()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mouseSenseSlider.value = mouseSenseData.sensitivity;
        //daysData.currentIndex = 0;
        CoinsData._amount = 0;
        _music= FMODUnity.RuntimeManager.CreateInstance("event:/Music/MenuMusic");
        _music.start();
    }
    
    private void OnDestroy()
    {
        _music.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void LoadGame()
    {
        StartCoroutine(TransitionToGame());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        mainMenu.alpha = 0;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = true;
        credits.alpha = 1;
        credits.interactable = true;
        credits.blocksRaycasts = true;
    }

    public void GoBack()
    {
        mainMenu.alpha = 1;
        mainMenu.interactable = true;
        mainMenu.blocksRaycasts = true;
        credits.alpha = 0;
        credits.interactable = false;
        credits.blocksRaycasts = false;
    }

    IEnumerator TransitionToGame()
    {
        mainMenu.interactable = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Intro");
    }


    public void SetMouseSense()
    {
        mouseSenseData.sensitivity = mouseSenseSlider.value;
    }
}
