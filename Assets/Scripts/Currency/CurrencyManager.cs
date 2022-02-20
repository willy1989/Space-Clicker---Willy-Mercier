using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private JsonDataUser<CurrencyPersistentData> jsonDataUser; 

    public float CurrencyCount
    {
        get
        {
            return jsonDataUser.JsonData.Currency;
        }

        private set
        {
            if(value < 0)
            {
                throw new ArgumentOutOfRangeException("The currency count can't be negative");
            }

            jsonDataUser.JsonData.Currency = value;
            if (UpdateCurrencyEvent != null)
                UpdateCurrencyEvent.Invoke();
        }
    }


    public Action UpdateCurrencyEvent;
    public Action<float> AddCurrencyEvent;



    private void Awake()
    {
        SetInstance();

        jsonDataUser = new JsonDataUser<CurrencyPersistentData>(_StartJsonData: new CurrencyPersistentData(_currency: 0),
                                                                _jsonFileName: "currencyData.json");
    }

    private void Start()
    {
        WaveManager.Instance.SpawnWaveAction += jsonDataUser.SaveData;
    }

    public void SpendCurrency(float amount)
    {
        CurrencyCount -= amount;
    }

    public void AddMoney(float amount)
    {
        CurrencyCount += amount;
        AddCurrencyEvent.Invoke(amount);
    }

    public bool HasSufficientBalance(float cost)
    {
        return (CurrencyCount - cost) >= 0;
    }
}
