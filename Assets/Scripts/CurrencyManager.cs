using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public float CurrencyCount
    {
        get
        {
            return PlayerPrefs.GetFloat(Constants.CurrencyCount_PlayerPref, 0);
        }

        set
        {
            // To do: throw exception when value is below 0.
            PlayerPrefs.SetFloat(Constants.CurrencyCount_PlayerPref, value);
        }
    }

    public void SpendCurrency(float ammount)
    {
        CurrencyCount -= ammount;
    }

    public bool HasSufficientBalance(float ammount)
    {
        return (CurrencyCount - ammount) > 0;
    }


    private void Awake()
    {
        SetInstance();
    }
}
