using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipShopUI : Singleton<SpaceShipShopUI>
{
    [SerializeField] private Text levelText;

    [SerializeField] private Text baseDamageText;

    [SerializeField] private Text baseFrequencyText;

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

        SpaceShipLevelManager.Instance.DamageIncreasedEvent += UpdateDamageText;

        SpaceShipLevelManager.Instance.FrequencyIncreasedEvent += UpdateFrequencyText;

        openShopButton.onClick.AddListener(ToggleUIelementsOn);

        closeShopButton.onClick.AddListener(ToggleUIelementsOff);

        UpdateLevelText();
        UpdateDamageText();
        UpdateFrequencyText();
    }

    private void UpdateLevelText()
    {
        levelText.text = "Ship level " + SpaceShipLevelManager.Instance.SpaceShipLevel.ToString();
    }

    private void UpdateDamageText()
    {
        float damage = SpaceShipLevelManager.Instance.SpaceShipBaseDamage;

        damage = Mathf.Round(damage * 100f) / 100f;

        baseDamageText.text = "Damage :" + damage.ToString();
    }

    private void UpdateFrequencyText()
    {
        float frequency = SpaceShipLevelManager.Instance.SpaceShipBaseFrequency;

        frequency = Mathf.Round(frequency * 100f) / 100f;

        baseFrequencyText.text = "Frequency :" + frequency.ToString();
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
