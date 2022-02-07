using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private float moveSpeed = 20f;

    private float damage;

    private Vector3 direction = Vector3.zero;

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    public void SetDirection(Target _target)
    {
        direction = (_target.transform.position - transform.position).normalized;
    }

    public void SetDirection(Vector2 _direction)
    {
        direction = _direction;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetSpawnPosition(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
    }

    private void MoveToTarget()
    {
        transform.position += direction * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Target_Tag) == true)
        {
            // TO DO: Add exception, the target gameobject must have a Target component
            Target target = other.GetComponent<Target>();
            target.TakeDamage(damage);

            Reset();
        }

        else if(other.CompareTag(Constants.Boundary_Tag) == true)
        {
            Reset();
        }
    }

    public void Reset()
    {
        damage = 0;

        direction = Vector2.zero;

        gameObject.SetActive(false);
    }
}
