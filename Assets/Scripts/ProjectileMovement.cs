using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private float moveSpeed = 5f;

    private int damage;

    private Vector3 direction = Vector2.zero;

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

    public void SetDamage(int _damage)
    {
        damage = _damage;
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

            Destroy(gameObject);
        }

        else if(other.CompareTag(Constants.Boundary_Tag) == true)
        {
            Destroy(gameObject);
        }
    }
}
