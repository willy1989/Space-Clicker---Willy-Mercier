using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int startHealth;

    [SerializeField] private int moveSpeed;

    [SerializeField] private int currencyValue;

    public int CurrentHealth { get; private set; }

    public Action DeathEvent;

    public Action UpdateHealthEvent;

    private void Awake()
    {
        CurrentHealth = startHealth;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.down * moveSpeed * Time.fixedDeltaTime;
    }

    public void TakeDamage(int damageTaken)
    {
        CurrentHealth -= damageTaken;

        if (UpdateHealthEvent != null)
            UpdateHealthEvent.Invoke();

        if (CurrentHealth <= 0)
        {
            if(DeathEvent!= null)
                DeathEvent.Invoke();
            Die();
        }
    }

    private void Die()
    {
        CurrencyManager.Instance.AddMoney(ammount: currencyValue);
        WaveManager.Instance.currentWave.RemoveTarget(this);
        Destroy(gameObject);
    }
}
