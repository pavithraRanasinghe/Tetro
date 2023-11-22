using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _scorePrefabs;
    [SerializeField] private List<Obstacle> _obstacles;
    [SerializeField] private Player _player;

    public int _score;
    private int _rotationUpdateValue = 10;

    private void Start()
    {
        _obstacles = GameObject.FindGameObjectsWithTag("Obstacle").Select(obj => obj.GetComponent<Obstacle>()).ToList();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Awake()
    {
        GameManager.Instance.IsInitialized = true;

        _score = 0;
        _scoreText.text = _score.ToString();
        SpawnScore();
    }

    public void UpdateScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
        SpawnScore();
    }

    private void SpawnScore()
    {
        Instantiate(_scorePrefabs);
    }

    public void GameEnded()
    {
        GameManager.Instance.CurrentScore = _score;
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GoToMainMenu();
    }

    public void ChangeRotation()
    {
        if (_score == _rotationUpdateValue)
        {
            foreach (Obstacle obstacle in _obstacles)
            {
                obstacle.ChangeRotation();
            }

            _player.ChangeRotation();

            int lastMin = _rotationUpdateValue;
            _rotationUpdateValue = Random.Range(lastMin + 2, lastMin + 10);
        }
    }
}