using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int startHealth;

    [SerializeField] private int moveSpeed;

    public int CurrentHealth { get; private set; }

    public Action DeathEvent;

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

        if (CurrentHealth <= 0)
        {
            DeathEvent.Invoke();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
