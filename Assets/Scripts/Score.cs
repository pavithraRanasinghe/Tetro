using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform _centerTransform;
    [SerializeField] private List<float> _spawnPosX;

    private void Awake()
    {
        transform.localPosition = Vector3.right * _spawnPosX[Random.Range(0, _spawnPosX.Count)];
        _centerTransform.rotation = Quaternion.Euler(0,0, Random.Range(0,37) * 10f);
    }
    
    public void ScoreAdded()
    {
        Destroy(_centerTransform.gameObject);
    }
}