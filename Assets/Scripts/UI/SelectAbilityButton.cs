using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAbilityButton : MonoBehaviour
{
    private Button button;

    private Image buttonImage;

    private Color originalColor;

    private Color highlightColor = new Color(0f, 0.9f, 0.9f);

    private static SelectAbilityButton currentSelectButton;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Button>().image;
        originalColor = buttonImage.color;

        button.onClick.AddListener(HighlightButton);
    }

    public void HighlightButton()
    {
        if (currentSelectButton != null)
        {
            currentSelectButton.DeHighlightButton();
        }

        buttonImage.color = highlightColor;

        currentSelectButton = this;


    }

    public void DeHighlightButton()
    {
        buttonImage.color = originalColor;
    }
}
