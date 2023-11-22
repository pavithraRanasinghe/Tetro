using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _rotaTransform;

    private void FixedUpdate()
    {
        _rotaTransform.Rotate(0, 0, _rotateSpeed * Time.fixedDeltaTime);
    }

    public void ChangeRotation()
    {
        _rotateSpeed = -_rotateSpeed;
    }
}