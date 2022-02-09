using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDamage : MonoBehaviour
{
    private int invincibilityTime = 5;

    private bool IsInvincible = false;

    private void Start()
    {
        BecomeInvincible();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Target_Tag) == true && IsInvincible == false)
        {
            SpaceShipRespawnManager.Instance.DespawnSpaceShip();
            SpaceShipRespawnManager.Instance.RespawnSpaceShip();
        }

        else if(collision.CompareTag(Constants.InvincibilityPowerUp_Tag) == true)
        {
            BecomeInvincible();
            PowerUpUI.Instance.ShowPowerUpMessage(Constants.PowerUpMessageInvicibility);
            Destroy(collision.gameObject);
        }
    }

    public void BecomeInvincible()
    {
        StartCoroutine(BecomeInvincibleCoroutine());
    }

    private IEnumerator BecomeInvincibleCoroutine()
    {
        IsInvincible = true;

        yield return new WaitForSeconds(invincibilityTime);

        IsInvincible = false;
    }
}
