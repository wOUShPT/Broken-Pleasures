using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public PowerUpSO powerUpData;
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
        coinText.text = coinData.amount.ToString();
        powderButton.interactable = false;
        javButton.interactable = false;
        magnetButton.interactable = false;
        if (coinData.amount >= 20)
        {
            powderButton.interactable = true;
        }

        if (coinData.amount >= 60)
        {
            javButton.interactable = true;
        }

        if (coinData.amount >= 80)
        {
            magnetButton.interactable = true;
        }
    }

    public void BuyPowerUp(int index)
    {
        switch (index)
        {
            case 0:

                if (coinData.amount >= 20)
                {
                    powerUpData.currentPowerUp = PowerUpSO.PowerUp.Powder;
                    coinData.DecreaseMoney(20);
                    powderButton.interactable = false;
                    javButton.interactable = false;
                    magnetButton.interactable = false;
                    coinText.text = coinData.amount.ToString();
                }
                break;
            
            case 1:

                if (coinData.amount >= 40)
                {
                    powerUpData.currentPowerUp = PowerUpSO.PowerUp.Jav;
                    coinData.DecreaseMoney(40);
                    powderButton.interactable = false;
                    javButton.interactable = false;
                    magnetButton.interactable = false;
                    coinText.text = coinData.amount.ToString();
                }

                break;
            
            case 2:

                if (coinData.amount >= 60)
                {
                    powerUpData.currentPowerUp = PowerUpSO.PowerUp.Magnet;
                    coinData.DecreaseMoney(60);
                    powderButton.interactable = false;
                    javButton.interactable = false;
                    magnetButton.interactable = false;
                    coinText.text = coinData.amount.ToString();
                }

                break;
        }
        
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
