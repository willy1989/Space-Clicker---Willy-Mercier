using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CurrencyPersistentData
{
    public float Currency;

    public CurrencyPersistentData(float _currency)
    {
        Currency = _currency;
    }
}
