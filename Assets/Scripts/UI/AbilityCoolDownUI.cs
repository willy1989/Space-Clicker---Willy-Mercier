using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCoolDownUI : MonoBehaviour
{
    [SerializeField] private SpaceShipAbilities ability;

    private Image buttonImage;

    private Color originalColor;

    private Color coolDownColor = Color.white;

    private void Awake()
    {
        buttonImage = GetComponent<Button>().image;
        originalColor = buttonImage.color;
        ability.StartAbilityCoolDownEvent += ButtonOpacityFadeIn;
    }

    private void ButtonOpacityFadeIn(float duration)
    {
        StartCoroutine(fadeInCoroutine(duration: duration));
    }

    private IEnumerator fadeInCoroutine(float duration)
    {
        buttonImage.color = coolDownColor;

        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            float fadedOpacity = Mathf.Lerp(0f, 1f, elapsedTime/ duration);

            buttonImage.color = new Color(coolDownColor.r, coolDownColor.g, coolDownColor.b, fadedOpacity);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        buttonImage.color = originalColor;
    }
}
