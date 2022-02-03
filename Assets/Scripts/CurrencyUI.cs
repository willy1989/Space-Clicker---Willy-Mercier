using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : Singleton<CurrencyUI>
{
    [SerializeField] private Text currentCurrencyText;


    private void UpdateCurrentCurrencyText(float currentCurrencyCount)
    {
        currentCurrencyText.text = Utils.ConvertNumberToShortText(currentCurrencyCount);
    }

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        CurrencyManager.Instance.UpdateCurrencyEvent += UpdateCurrentCurrencyText;
    }
}
