using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : Singleton<CurrencyUI>
{
    [SerializeField] private Text currentCurrencyText;


    private void UpdateCurrentCurrencyText(float currentCurrencyCount)
    {
        currentCurrencyText.text = Utils.AbreviateNumber(currentCurrencyCount);
    }

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        UpdateCurrentCurrencyText(CurrencyManager.Instance.CurrencyCount);
        CurrencyManager.Instance.UpdateCurrencyEvent += UpdateCurrentCurrencyText;
    }
}
