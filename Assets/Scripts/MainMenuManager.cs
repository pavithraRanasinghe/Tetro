using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _newBestText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private List<GameObject> _highScorePrefabs;
    public Transform centerTransform;

    public GameObject bannerAdObj;
    private BannerAd _bannerAd;

    private void Awake()
    {
        _bestScoreText.text = GameManager.Instance.HighScore.ToString();

        _bannerAd = bannerAdObj.GetComponent<BannerAd>();
        _bannerAd.LoadAd();
        if(!GameManager.Instance.IsInitialized)
        {
            _scoreText.gameObject.SetActive(false);
            _newBestText.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowScore());
        }
    }

    [SerializeField] private float _animationTime;
    [SerializeField] private AnimationCurve _speedCurve;

    private IEnumerator ShowScore()
    {
        int tempScore = 0;
        _scoreText.text = tempScore.ToString();

        int currentScore = GameManager.Instance.CurrentScore;
        int highScore = GameManager.Instance.HighScore;

        if(currentScore > highScore)
        {
            _newBestText.gameObject.SetActive(true);
            GameManager.Instance.HighScore = currentScore;
            foreach (GameObject _highScorePrefab in _highScorePrefabs)
            {
                Instantiate(_highScorePrefab, centerTransform.position, Quaternion.identity);
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
            _scoreText.text = tempScore.ToString();

            yield return null;
        }

        tempScore = currentScore;
        _scoreText.text = tempScore.ToString();
    }

    [SerializeField] private AudioClip _clickSound;

    public void ClickedPlay()
    {
        _bannerAd.DestroyBannerAd();
        SoundManager.Instance.PlaySound(_clickSound);
        GameManager.Instance.GoToGameplay();
    }




}
