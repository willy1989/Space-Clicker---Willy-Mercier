using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : Singleton<CurrencyUI>
{
    [SerializeField] private Text currentCurrencyText;


    private void UpdateCurrentCurrencyText()
    {
        currentCurrencyText.text = Utils.AbreviateNumber(CurrencyManager.Instance.CurrencyCount);
    }

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        UpdateCurrentCurrencyText();
        CurrencyManager.Instance.UpdateCurrencyEvent += UpdateCurrentCurrencyText;
    }
}
