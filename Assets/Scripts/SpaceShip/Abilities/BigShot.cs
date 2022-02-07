using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShot : MonoBehaviour
{
    private float damage;

    private float movementSpeed = 5f;

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.up * movementSpeed * Time.fixedDeltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Target_Tag) == true)
        {
            // TO DO: Add exception, the target gameobject must have a Target component
            Target target = other.GetComponent<Target>();
            target.TakeDamage(damage);
        }

        else if (other.CompareTag(Constants.Boundary_Tag) == true)
        {
            Destroy(gameObject);
        }
    }
}
