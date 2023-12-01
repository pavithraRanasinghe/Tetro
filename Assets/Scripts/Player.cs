using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _moveClip, _loseClip, _pointClip;

    [SerializeField] private GameplayManager _gm;
    [SerializeField] private GameObject _explosionPrefab, _scoreParticlePrefab;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _rotateTransform;
    public GameOverController _gameOverController;


    private bool _canClick;
    public bool isStealth;
    public bool isActiveScoreAbility;
    
    private void Awake()
    {
        isStealth = false;
        isActiveScoreAbility = false;
        _canClick = true;
        level = 0;
        currentRadius = _startRadius;
    }
    
    private bool IsPointerOverUI()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        if (!IsPointerOverUI())
        {
            if(_canClick && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ChangeRadius());
                SoundManager.Instance.PlaySound(_moveClip);
            }   
        }
    }
    private void FixedUpdate()
    {
        transform.localPosition = Vector3.up * currentRadius;
        float rotateValue = _rotateSpeed * Time.fixedDeltaTime * _startRadius / currentRadius;
        _rotateTransform.Rotate(0, 0, rotateValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle") && !isStealth)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(_loseClip);
            _gameOverController.GameOver();
            return;
        }

        if(collision.CompareTag("Score"))
        {
            Destroy(Instantiate(_scoreParticlePrefab, transform.position, Quaternion.identity),1f);
            SoundManager.Instance.PlaySound(_pointClip);
            _gm.UpdateScore(isActiveScoreAbility);
            collision.gameObject.GetComponent<Score>().ScoreAdded();
            _gm.ChangeRotation();
            return;
        }

        if (collision.CompareTag("Ability"))
        {
            gameObject.GetComponent<AbilityHolder>().ActivateAbility(collision.gameObject.name);
            collision.gameObject.GetComponent<AbilityPicker>().Picked();
        }
    }
    
    [SerializeField] private float _startRadius;
    [SerializeField] private float _moveTime;

    [SerializeField] private List<float> _rotateRadius;
    private float currentRadius;
    private int level;
    
    private IEnumerator ChangeRadius()
    {
        _canClick = false;
        float moveStartRadius = _rotateRadius[level];
        float moveEndRadius = _rotateRadius[(level + 1) % _rotateRadius.Count];
        float moveOffset = moveEndRadius - moveStartRadius;
        float speed = 1 / _moveTime;
        float timeElasped = 0f;
        while(timeElasped < 1f)
        {
            timeElasped += speed * Time.fixedDeltaTime;
            currentRadius = moveStartRadius + timeElasped * moveOffset;
            yield return new WaitForFixedUpdate();
        }

        _canClick = true;
        level = (level + 1) % _rotateRadius.Count;
        currentRadius = _rotateRadius[level];
    }
    
    public void ChangeRotation()
    {
        _rotateSpeed = -_rotateSpeed;
    }
}