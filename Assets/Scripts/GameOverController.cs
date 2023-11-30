using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pauseButton;
    public GameObject player;
    public GameObject continueButton;
    private bool _istried = false;

    [SerializeField] private GameObject stealthAbility;
    [SerializeField] private GameplayManager _gameplayManager;
    public void GameOver()
    {
        if (!_istried)
        {
            gameOverPanel.SetActive(true);
            pauseButton.SetActive(false);
            
            GameObject timer = GameObject.FindGameObjectWithTag("Continue_Timer");
            Image timerImage = timer.GetComponent<Image>();
            timerImage.enabled = true;
            AbilityTimer abilityTimer = timer.GetComponent<AbilityTimer>();
            abilityTimer.ActivateTimer(5.0f);
            StartCoroutine(TimerOver(timerImage));
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<ParticleSystem>().Stop();
            _istried = true;
        }
        else
        {
            _gameplayManager.GameEnded();
        }
    }

    public void ContinueGame()
    {
        player.GetComponent<AbilityHolder>().ActivateAbility("stealth");
        gameOverPanel.SetActive(false);
        pauseButton.SetActive(true);
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<ParticleSystem>().Play();
        Time.timeScale = 1;
    }
    
    IEnumerator TimerOver(Image timerImage)
    {
        yield return new WaitForSeconds(5);
        timerImage.enabled = false;
        continueButton.SetActive(false);
        
        _gameplayManager.GameEnded();
    }
}