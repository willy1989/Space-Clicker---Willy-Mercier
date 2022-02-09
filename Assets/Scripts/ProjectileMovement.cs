using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundPlayer))]
public class ProjectileMovement : MonoBehaviour
{
    private float moveSpeed = 20f;

    private float damage;

    private Vector3 direction = Vector3.zero;


    private void FixedUpdate()
    {
        MoveToTarget();
    }


    public void SetData(Vector2 _direction, float _damage, Vector2 _spawnPosition)
    {
        direction = _direction.normalized;
        damage = _damage;
        transform.position = _spawnPosition;
    }

    private void MoveToTarget()
    {
        transform.position += direction * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Target_Tag) == true)
        {
            Target target = other.GetComponent<Target>();
            if (target == null)
                return;

            target.TakeDamage(damage);

            Reset();
        }

        else if(other.CompareTag(Constants.Boundary_Tag) == true)
            Reset();
    }

    public void Reset()
    {
        damage = 0;

        direction = Vector2.zero;

        gameObject.SetActive(false);
    }
}
