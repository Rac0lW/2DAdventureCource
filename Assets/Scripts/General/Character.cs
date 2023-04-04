using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Base Status")]
    public float maxHealth;
    public float CurrentHealth;

    [Header("Invulnerable Status")]

    public float invulnerableDuration;

    [HideInInspector]public float invulnerableCounter;

    public bool invulnerable;

    public UnityEvent<Transform> OnTakeDamege;
    public UnityEvent OnDead;

    [Header("Event Go Go Go")]
    public UnityEvent<Character> OnHealthChange;
    private void Start() {
        CurrentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }

    private void Update(){
        if(invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if(invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamege(Attack attacker)
    {
            if (invulnerable){
                return;
            }
        // Debug.Log(attacker.demage);
        if(CurrentHealth - attacker.demage > 0)
        {
            CurrentHealth -= attacker.demage;
            TriggerInvulnerable();
            //trigger the hurt animation
            OnTakeDamege?.Invoke(attacker.transform);
        }
        else{
        // trigger death status
            CurrentHealth = 0;
            OnDead?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }

    private void TriggerInvulnerable()
    {
        if(!invulnerable){
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
