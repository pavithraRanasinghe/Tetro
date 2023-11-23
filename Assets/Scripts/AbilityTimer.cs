using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTimer : MonoBehaviour
{
    [SerializeField] private Image _uiFill;
    private float _duration;
    private int _durationInt;
    

    private IEnumerator UpdateTimer()
    {
        while (_duration > 0)
        {
            _duration -= Time.deltaTime;
            _uiFill.fillAmount = _duration / _durationInt;
            yield return null;
        }
    }

    public void ActivateTimer(float duration)
    {
        _duration = duration;
        _durationInt = (int) duration;
        StartCoroutine(UpdateTimer());
    }
}
