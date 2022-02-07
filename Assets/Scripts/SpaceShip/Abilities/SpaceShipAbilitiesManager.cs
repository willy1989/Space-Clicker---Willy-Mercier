using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilitiesManager : Singleton<SpaceShipAbilitiesManager>
{
    [SerializeField] private SpaceShipAbilities currentAbility;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        SpaceShipInput.Instance.DoubleTapEvent += UseAbility;
    }

    private void UseAbility()
    {
        if(currentAbility != null)
            currentAbility.UseAbility();
    }

    public void SetCurrentAbility(SpaceShipAbilities ability)
    {
        currentAbility = ability;
    }
}
