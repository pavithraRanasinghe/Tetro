using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _newBestText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _totalScoreText;
    [SerializeField] private List<GameObject> _highScorePrefabs;
    public Transform centerTransform;

    public GameObject bannerAdObj;
    private BannerAd _bannerAd;
    public bool isPauseGame = false;

    private void Awake()
    {
        _bestScoreText.text = GameManager.Instance.HighScore.ToString();
        _totalScoreText.text = GameManager.Instance.TotalScore.ToString();
        if(!GameManager.Instance.IsInitialized)
        {
            _scoreText.gameObject.SetActive(false);
            _newBestText.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowScore());
        }
        
        _bannerAd = bannerAdObj.GetComponent<BannerAd>();
        _bannerAd.LoadBannerAd();
    }

    [SerializeField] private float _animationTime;
    [SerializeField] private AnimationCurve _speedCurve;

    private IEnumerator ShowScore()
    {
        int currentScore = 0;
        int highScore = GameManager.Instance.HighScore;
        // If pressed main menu button
        if (!isPauseGame)
        {
            currentScore = GameManager.Instance.CurrentScore;
            highScore = GameManager.Instance.HighScore;
            // Calculation in the Set method
            GameManager.Instance.TotalScore = currentScore;
        }
        else
        {
            isPauseGame = false;
        }
        int tempScore = 0;
        int tempTotalScore = 0;
        _scoreText.text = tempScore.ToString();
        _totalScoreText.text = tempTotalScore.ToString();
        
        if(currentScore > highScore)
        {
            _newBestText.gameObject.SetActive(true);
            GameManager.Instance.HighScore = currentScore;
            foreach (GameObject highScorePrefab in _highScorePrefabs)
            {
                Instantiate(highScorePrefab, centerTransform.position, Quaternion.identity);
            }
        }
        else
        {
            _newBestText.gameObject.SetActive(false);
        }

        float speed = 1 / _animationTime;
        float timeElapsed = 0f;
        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;

            tempScore = (int)(_speedCurve.Evaluate(timeElapsed) * currentScore);
            tempTotalScore = (int)(_speedCurve.Evaluate(timeElapsed) * GameManager.Instance.TotalScore);
            _scoreText.text = tempScore.ToString();
            _totalScoreText.text = tempTotalScore.ToString();
            yield return null;
        }

        tempScore = currentScore;
        _scoreText.text = tempScore.ToString();
        _totalScoreText.text = GameManager.Instance.TotalScore.ToString();
    }

    [SerializeField] private AudioClip _clickSound;

    public void ClickedPlay()
    {
        _bannerAd.DestroyBannerAd();
        SoundManager.Instance.PlaySound(_clickSound);
        GameManager.Instance.GoToGameplay();
    }
}
