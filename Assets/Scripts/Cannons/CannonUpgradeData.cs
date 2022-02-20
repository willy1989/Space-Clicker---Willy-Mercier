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

    private JsonDataUser<CannonUpgradePersistentData> jsonDataUser;

    public float ShootingFrequency
    {
        get
        {
            return jsonDataUser.JsonData.ShootingFrequency;
        }
    }

    public int Damage
    {
        get
        {
            return jsonDataUser.JsonData.Damage;
        }
    }

    public float NextDamageUpGradeCost
    {
        get
        {
            return jsonDataUser.JsonData.NextDamageUpGradeCost;
        }
    }

    public float NextFrequencyUpGradeCost
    {
        get
        {
            return jsonDataUser.JsonData.NextFrequencyUpGradeCost;
        }
    }

    private void Awake()
    {
        GetJsonFileName();
        CannonUpgradePersistentData cannonUpgradePersistentData = new CannonUpgradePersistentData(_ShootingFrequency: startShootingFrequency, 
                                                                                              _Damage: startDamage, 
                                                                                              _NextDamageUpGradeCost: startNextDamageUpGradeCost, 
                                                                                              _NextFrequencyUpGradeCost: startNextFrequencyUpGradeCost);

        jsonDataUser = new JsonDataUser<CannonUpgradePersistentData>(_StartJsonData: cannonUpgradePersistentData, _jsonFileName: GetJsonFileName());
    }

    private void Start()
    {
        WaveManager.Instance.SpawnWaveAction += jsonDataUser.SaveData;
    }

    private string GetJsonFileName()
    {
        return cannonNameDictionnary[cannon] + ".json";
    }

    public void UpGradeDamage()
    {
        if (CurrencyManager.Instance.HasSufficientBalance(NextDamageUpGradeCost) == false)
            return;

        CurrencyManager.Instance.SpendCurrency(NextDamageUpGradeCost);

        jsonDataUser.JsonData.Damage *= damageImprovement;
        jsonDataUser.JsonData.NextDamageUpGradeCost *= damageUpgradeCostRate;
    }

    public void UpGradeFrequency()
    {
        if (CurrencyManager.Instance.HasSufficientBalance(NextFrequencyUpGradeCost) == false)
            return;

        CurrencyManager.Instance.SpendCurrency(NextFrequencyUpGradeCost);

        jsonDataUser.JsonData.ShootingFrequency += shootingFrequencyImprovement;
        jsonDataUser.JsonData.NextFrequencyUpGradeCost *= frequencyUpgradeCostRate;
    }
}

public enum CannonName
{
    Cannon1,
    Cannon2,
    Cannon3,
    Cannon4
}
