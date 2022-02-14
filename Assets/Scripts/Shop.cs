using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Button powderButton;
    public Button javButton;
    public Button magnetButton;
    public Animator sceneTransitionAnimator;
    public CoinsSO coinData;
    public TextMeshProUGUI coinText;
    public Texture2D cursorImage;

    private void Awake()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        coinText.text = GameData.Coins.Amount.ToString();
        powderButton.interactable = false;
        javButton.interactable = false;
        magnetButton.interactable = false;
        if (GameData.Coins.Amount >= 20)
        {
            powderButton.interactable = true;
        }
    }

    public void BuyPowerUp(int index)
    {
        switch (index)
        {
            case 0:

                if (GameData.Coins.Amount >= 20)
                {
                    GameData.PowerUp = GameData.PowerUpType.Powder;
                    GameData.Coins.DecreaseMoney(20);
                }
                break;
            
            case 1:

                if (GameData.Coins.Amount >= 40)
                {
                    GameData.PowerUp = GameData.PowerUpType.Jav;
                    GameData.Coins.DecreaseMoney(40);
                }

                break;
            
            case 2:

                if (GameData.Coins.Amount >= 60)
                {
                    GameData.PowerUp = GameData.PowerUpType.Magnet;
                    GameData.Coins.DecreaseMoney(60);
                }

                break;
        }
        
        powderButton.interactable = false;
        javButton.interactable = false;
        magnetButton.interactable = false;
        coinText.text = GameData.Coins.Amount.ToString();
        
    }

    public void GoToNextDay()
    {
        StartCoroutine(TransitionToNextDayCoroutine());
    }

    IEnumerator TransitionToNextDayCoroutine()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
