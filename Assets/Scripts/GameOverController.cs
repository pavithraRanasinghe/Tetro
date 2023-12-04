using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pauseButton;
    public GameObject player;
    public GameObject continueButton;
    public GameObject bannerAdObj;
    public GameObject rewardedAdObj;
    [SerializeField] private GameplayManager _gameplayManager;

    private BannerAd _bannerAd;
    private RewardedAdController _rewardedAdController;


    private bool _istried = false;

    private void Start()
    {
        _bannerAd = bannerAdObj.GetComponent<BannerAd>();
        _rewardedAdController = rewardedAdObj.GetComponent<RewardedAdController>();

        _bannerAd.LoadBannerAd();
        _rewardedAdController.LoadRewardedAd();
    }

    public void GameOver()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
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
                player.SetActive(false);
                _istried = true;
            }
            else
            {
                _gameplayManager.GameEnded();
            }
        }
        else
        {
            _gameplayManager.GameEnded();
        }
    }

    public void PlayRewardedAd()
    {
        if (_rewardedAdController.CanShowAd())
        {
            _rewardedAdController.ShowRewardedAd();
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
        player.SetActive(true);
        Time.timeScale = 1;
        _bannerAd.DestroyBannerAd();
    }

    IEnumerator TimerOver(Image timerImage)
    {
        yield return new WaitForSeconds(5);
        timerImage.enabled = false;
        continueButton.SetActive(false);

        _gameplayManager.GameEnded();
    }
}