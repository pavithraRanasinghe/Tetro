using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject player;

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        player.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void ContinueGame()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        player.SetActive(true);
        Time.timeScale = 1;
    }

    public void GotoMainMenu()
    {
        GameManager.Instance.GoToMainMenu();
        Time.timeScale = 1;
    }
}
