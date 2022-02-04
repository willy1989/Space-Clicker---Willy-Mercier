using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDamage : MonoBehaviour
{
    private int invincibilityTime = 10;

    private bool IsInvincible = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Target_Tag) == true && IsInvincible == false)
        {
            Destroy(transform.parent.gameObject);
        }

        else if(collision.CompareTag(Constants.InvincibilityPowerUp_Tag) == true)
        {
            StartCoroutine(BecomeInvincible());
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator BecomeInvincible()
    {
        IsInvincible = true;

        yield return new WaitForSeconds(invincibilityTime);

        IsInvincible = false;
    }
}
