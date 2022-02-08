using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : Singleton<PowerUpUI>
{
    private Text powerUpMessage;

    private Animator animator;


    private void Awake()
    {
        SetInstance();

        powerUpMessage = GetComponentInChildren<Text>();

        animator = GetComponent<Animator>();
    }

    public void ShowPowerUpMessage(string message)
    {
        powerUpMessage.text = message;

        animator.SetTrigger(Constants.PowerUpShow_AnimationTrigger);
    }
}
