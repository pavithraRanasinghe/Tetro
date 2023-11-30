using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    [SerializeField] private Transform _rotateTransform;

    private void FixedUpdate()
    {
        _rotateTransform.Rotate(0, 0, 150 * Time.fixedDeltaTime);
    }
}
