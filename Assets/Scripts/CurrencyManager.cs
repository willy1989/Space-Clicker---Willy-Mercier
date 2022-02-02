using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public int CurrencyCount
    {
        get
        {
            return PlayerPrefs.GetInt(Constants.CurrencyCount_PlayerPref, 0);
        }

        set
        {
            PlayerPrefs.SetInt(Constants.CurrencyCount_PlayerPref, value);
        }
    }


    private void Awake()
    {
        SetInstance();
    }
}
