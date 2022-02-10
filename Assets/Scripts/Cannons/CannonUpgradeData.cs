using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonUpgradeData : MonoBehaviour
{
    [SerializeField] private CannonName cannon;

    private string cannonName;

    private const float startShootingFrequency = 1f;
    private const int startDamage = 1;
    private const float startNextDamageUpGradeCost = 1f;
    private const float startNextFrequencyUpGradeCost = 1f;

    private Dictionary<CannonName, string> cannonNameDictionnary = new Dictionary<CannonName, string>
    {
        {CannonName.Cannon1, Constants.CannonName1_PlayerPref},
        {CannonName.Cannon2, Constants.CannonName2_PlayerPref},
        {CannonName.Cannon3, Constants.CannonName3_PlayerPref},
        {CannonName.Cannon4, Constants.CannonName4_PlayerPref}
    };

    private void Awake()
    {
        cannonName = cannonNameDictionnary[cannon];
    }

    public float ShootingFrequency
    {
        get
        {
            return PlayerPrefs.GetFloat(cannonName + "ShootingFrequency", startShootingFrequency);
        }

        set
        {
            PlayerPrefs.SetFloat(cannonName + "ShootingFrequency", value);
        }
    }

    public int Damage
    {
        get
        {
            return PlayerPrefs.GetInt(cannonName + "Damage", startDamage);
        }

        set
        {
            PlayerPrefs.SetInt(cannonName + "Damage", value);
        }
    }

    public float NextDamageUpGradeCost
    {
        get
        {
            return PlayerPrefs.GetFloat(cannonName + "NextDamageUpGradeCost", startNextDamageUpGradeCost);
        }

        set
        {
            PlayerPrefs.SetFloat(cannonName + "NextDamageUpGradeCost", value);
        }
    }

    public float NextFrequencyUpGradeCost
    {
        get
        {
            return PlayerPrefs.GetFloat(cannonName + "NextFrequencyUpGradeCost", startNextFrequencyUpGradeCost);
        }

        set
        {
            PlayerPrefs.SetFloat(cannonName + "NextFrequencyUpGradeCost", value);
        }
    }

    public void UpGradeDamage()
    {
        if (CurrencyManager.Instance.HasSufficientBalance(NextDamageUpGradeCost) == false)
            return;

        CurrencyManager.Instance.SpendCurrency(NextDamageUpGradeCost);

        Damage *= 2;
        NextDamageUpGradeCost *= 5;
    }

    public void UpGradeFrequency()
    {
        if (CurrencyManager.Instance.HasSufficientBalance(NextFrequencyUpGradeCost) == false)
            return;

        CurrencyManager.Instance.SpendCurrency(NextFrequencyUpGradeCost);

        ShootingFrequency -= 0.05f;
        NextFrequencyUpGradeCost *= 5f;
    }
}

public enum CannonName
{
    Cannon1,
    Cannon2,
    Cannon3,
    Cannon4
}
