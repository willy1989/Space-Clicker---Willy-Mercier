using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CannonUpgradeData : MonoBehaviour
{
    private static readonly float startShootingFrequency = 1f;
    private static readonly int startDamage = 1;
    private static readonly float startNextDamageUpGradeCost = 1f;
    private static readonly float startNextFrequencyUpGradeCost = 1f;

    private static readonly float shootingFrequencyImprovement = -0.05f;
    private static readonly float frequencyUpgradeCostRate = 5f;

    private static readonly int damageImprovement = 2;
    private static readonly float damageUpgradeCostRate = 5f;

    private static readonly Dictionary<CannonName, string> cannonNameDictionnary = new Dictionary<CannonName, string>
    {
        {CannonName.Cannon1, Constants.CannonName1},
        {CannonName.Cannon2, Constants.CannonName2},
        {CannonName.Cannon3, Constants.CannonName3},
        {CannonName.Cannon4, Constants.CannonName4}
    };

    [SerializeField] private CannonName cannon;

    private CannonUpgradePersistentData cannonUpgradePersistentData;

    private string jsonFileName;

    public float ShootingFrequency
    {
        get
        {
            return cannonUpgradePersistentData.ShootingFrequency;
        }
    }

    public int Damage
    {
        get
        {
            return cannonUpgradePersistentData.Damage;
        }
    }

    public float NextDamageUpGradeCost
    {
        get
        {
            return cannonUpgradePersistentData.NextDamageUpGradeCost;
        }
    }

    public float NextFrequencyUpGradeCost
    {
        get
        {
            return cannonUpgradePersistentData.NextFrequencyUpGradeCost;
        }
    }

    private void Awake()
    {
        SetPath();
        LoadCannonData();
    }

    private void SetPath()
    {
        jsonFileName = cannonNameDictionnary[cannon] + ".json";
    }

    private void LoadCannonData()
    {
        if (JsonDataManagement.FileExists(jsonFileName) == true)
        {
            cannonUpgradePersistentData = JsonDataManagement.LoadData<CannonUpgradePersistentData>(jsonFileName);
        }

        else
        {
            cannonUpgradePersistentData = new CannonUpgradePersistentData(_ShootingFrequency: startShootingFrequency,
                                                                          _Damage: startDamage,
                                                                          _NextDamageUpGradeCost: startNextDamageUpGradeCost,
                                                                          _NextFrequencyUpGradeCost: startNextFrequencyUpGradeCost);
        }
    }

    public void UpGradeDamage()
    {
        if (CurrencyManager.Instance.HasSufficientBalance(NextDamageUpGradeCost) == false)
            return;

        CurrencyManager.Instance.SpendCurrency(NextDamageUpGradeCost);

        cannonUpgradePersistentData.Damage *= damageImprovement;
        cannonUpgradePersistentData.NextDamageUpGradeCost *= damageUpgradeCostRate;

        JsonDataManagement.SaveData<CannonUpgradePersistentData>(fileName: jsonFileName, data: cannonUpgradePersistentData);
    }

    public void UpGradeFrequency()
    {
        if (CurrencyManager.Instance.HasSufficientBalance(NextFrequencyUpGradeCost) == false)
            return;

        CurrencyManager.Instance.SpendCurrency(NextFrequencyUpGradeCost);

        cannonUpgradePersistentData.ShootingFrequency += shootingFrequencyImprovement;
        cannonUpgradePersistentData.NextFrequencyUpGradeCost *= frequencyUpgradeCostRate;

        JsonDataManagement.SaveData<CannonUpgradePersistentData>(fileName: jsonFileName, data: cannonUpgradePersistentData);
    }
}

public enum CannonName
{
    Cannon1,
    Cannon2,
    Cannon3,
    Cannon4
}
