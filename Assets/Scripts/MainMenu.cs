using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup mainMenu;

    public CanvasGroup credits;

    public Animator _sceneTransitionAnimator;

    public Texture2D cursorImage;


    private void Awake()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }
}
