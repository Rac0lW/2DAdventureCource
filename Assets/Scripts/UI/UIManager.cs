using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;

    [Header("Events Listener")]
    public CharacterEventSO healthEvent;

    private void OnEnable() {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable() {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character arg0)
    {
        // throw new NotImplementedException();
        var persent = arg0.CurrentHealth / arg0.maxHealth;
        playerStatBar.OnHealthChange(persent);
    }
}
