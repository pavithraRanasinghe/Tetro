using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _scorePrefabs;
    private List<Obstacle> _obstacles; 
    private Player _player;
    [SerializeField] private List<GameObject> _abilityPrefabs;


    public int score;
    private int _rotationUpdateValue;
    private int _abilityUpdateValue;

    public bool readyAbility;
    private float _abilityDuration = 5.0f;

    private void Start()
    {
        _obstacles = GameObject.FindGameObjectsWithTag("Obstacle").Select(obj => obj.GetComponent<Obstacle>()).ToList();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Awake()
    {
        GameManager.Instance.IsInitialized = true;

        _rotationUpdateValue = Random.Range(8, 15);
        _abilityUpdateValue = Random.Range(3, 8);
        readyAbility = true;
        score = 0;
        _scoreText.text = score.ToString();
        SpawnScore();
    }

    private void Update()
    {
        if (score == _abilityUpdateValue && readyAbility)
        {
            SpawnAbility();
            int lastMin = _abilityUpdateValue;
            _abilityUpdateValue = Random.Range(lastMin + 8, lastMin + 15);
        }
    }

    public void UpdateScore(bool isActiveScoreAbility)
    {
        if (isActiveScoreAbility)
        {
            score += 2;
        }
        else
        {
            score++;
        }

        _scoreText.text = score.ToString();
        SpawnScore();
    }

    private void SpawnScore()
    {
        Instantiate(_scorePrefabs);
    }

    private void SpawnAbility()
    {
        int selectedAbilityIndex = Random.Range(0, _abilityPrefabs.Count);
        GameObject ability = Instantiate(_abilityPrefabs[selectedAbilityIndex]);
        StartCoroutine(UpdateTimer(ability));
    }

    public void GameEnded()
    {
        GameManager.Instance.CurrentScore = score;
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GoToMainMenu();
    }

    public void ChangeRotation()
    {
        if (score == _rotationUpdateValue)
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

    private IEnumerator UpdateTimer(GameObject ability)
    {
        while (_abilityDuration > 0)
        {
            _abilityDuration -= Time.deltaTime;
            yield return null;
        }

        Destroy(ability);
        _abilityDuration = 5.0f;
    }
}