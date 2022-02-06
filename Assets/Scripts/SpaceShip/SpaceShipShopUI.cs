using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipShopUI : Singleton<SpaceShipShopUI>
{
    [SerializeField] private Text levelText;

    [SerializeField] private GameObject toggleableUIelements;

    [SerializeField] private Button openShopButton;

    [SerializeField] private Button closeShopButton;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        SpaceShipLevelManager.Instance.LevelIncreasedEvent += UpdateLevelText;

        openShopButton.onClick.AddListener(ToggleUIelementsOn);

        closeShopButton.onClick.AddListener(ToggleUIelementsOff);
    }

    private void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level.ToString();
    }

    public void ToggleUIelementsOn()
    {
        toggleableUIelements.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ToggleUIelementsOff()
    {
        toggleableUIelements.SetActive(false);

        Time.timeScale = 1f;
    }
}
