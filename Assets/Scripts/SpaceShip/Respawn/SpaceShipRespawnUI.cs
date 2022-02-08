using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipRespawnUI : Singleton<SpaceShipRespawnUI>
{
    private Text respawnCountDownText;

    private int countDownUpdateDelay = 1;

    private void Awake()
    {
        SetInstance();

        respawnCountDownText = GetComponentInChildren<Text>();

        respawnCountDownText.text = string.Empty;
    }

    public void StartRespawnCountDownText(float countDownDuration)
    {
        StartCoroutine(countDownCoroutine(countDownDuration: countDownDuration));
    }

    private IEnumerator countDownCoroutine(float countDownDuration)
    {
        float timeLeft = countDownDuration;

        while(timeLeft > 0f)
        {
            respawnCountDownText.text = timeLeft.ToString();

            yield return new WaitForSeconds(countDownUpdateDelay);

            timeLeft -= countDownUpdateDelay;
        }

        respawnCountDownText.text = string.Empty;
    }
}
