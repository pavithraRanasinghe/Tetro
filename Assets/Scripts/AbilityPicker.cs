using System.Collections.Generic;
using UnityEngine;

public class AbilityPicker : MonoBehaviour
{
    [SerializeField] private Transform _centerTransform;
    [SerializeField] private List<float> _spawnPosX;

    private void Awake()
    {
        transform.localPosition = Vector3.right * _spawnPosX[Random.Range(0, _spawnPosX.Count)];
        _centerTransform.rotation = Quaternion.Euler(0,0, Random.Range(0,37) * 10f);
    }

    public void Picked()
    {
        Destroy(_centerTransform.gameObject);
    }
}
