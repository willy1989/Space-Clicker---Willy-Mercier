using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int startHealth;

    [SerializeField] private int moveSpeed;

    [SerializeField] private int currencyValue;

    public float CurrentHealth { get; private set; }

    private bool canMove = true;

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
        if (canMove == false)
            return;

        transform.position += Vector3.down * moveSpeed * Time.fixedDeltaTime;
    }

    public void TakeDamage(float damageTaken)
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
        CurrencyManager.Instance.AddMoney(amount: currencyValue);
        WaveManager.Instance.CurrentWave.RemoveTarget(this);
        Destroy(gameObject);
    }

    public void ToggleMovement()
    {
        canMove = !canMove;
    }
}
