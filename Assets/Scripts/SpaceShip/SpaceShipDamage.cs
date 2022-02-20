using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDamage : MonoBehaviour
{
    [SerializeField] private SoundPlayer powerUpSoundPlayer;

    [SerializeField] private SpriteRenderer invicibilitySprite;

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
            powerUpSoundPlayer.PlaySoundEffect();
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

        float elapsedTime = 0f;

        ChangeInviciblitySpriteOpacity(opacity: 1f);

        while (elapsedTime < invincibilityTime)
        {
            elapsedTime += Time.deltaTime;

            ChangeInviciblitySpriteOpacity(elapsedTime, invincibilityTime);

            yield return null;
        }

        IsInvincible = false;
    }

    private void ChangeInviciblitySpriteOpacity(float elapsedInvincibilityTime, float invincibilityTotalDuration)
    {
        float opacity = Mathf.Lerp(1f, 0f, elapsedInvincibilityTime/ invincibilityTotalDuration);
        invicibilitySprite.color = new Color(invicibilitySprite.color.r, 
                                             invicibilitySprite.color.g, 
                                             invicibilitySprite.color.b,
                                             opacity);

    }

    private void ChangeInviciblitySpriteOpacity(float opacity)
    {
        invicibilitySprite.color = new Color(invicibilitySprite.color.r,
                                             invicibilitySprite.color.g,
                                             invicibilitySprite.color.b,
                                             opacity);
    }
}
