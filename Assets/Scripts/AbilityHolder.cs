using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private GameplayManager _gameplayManager;  
    private Ability _ability;
    public List<Ability> abilities;
    private float _cooldownTime;
    private float _activeTime;

    enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    private AbilityState _state = AbilityState.Cooldown;

    void Update()
    {
        switch (_state)
        {
            case AbilityState.Ready:
                _gameplayManager.readyAbility = false;
                
                GameObject timer = GameObject.FindGameObjectWithTag("Timer");
                timer.GetComponent<Image>().enabled = true;
                AbilityTimer abilityTimer = timer.GetComponent<AbilityTimer>();
                
                _ability.Active(gameObject);
                _state = AbilityState.Active;
                _activeTime = _ability.activeTime;
                abilityTimer.ActivateTimer(_activeTime);
                break;
            case AbilityState.Active:
                if (_activeTime > 0)
                {
                    _activeTime -= Time.deltaTime;
                }
                else
                {
                    _ability.Cooldown(gameObject);
                    _state = AbilityState.Cooldown;
                }

                break;
            case AbilityState.Cooldown:
                _gameplayManager.readyAbility = true;
                break;
        }
    }

    public void FindAbility(GameObject pickedAbility)
    {
        _ability = abilities.Find(ability => ability.name == pickedAbility.name);
        _state = AbilityState.Ready;
    }

}