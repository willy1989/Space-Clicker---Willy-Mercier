using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private List<Target> targets;

    public Action LastTargetKilledEvent;

    public List<Target> Targets
    {
        get
        {
            return targets;
        }
    }

    private void Awake()
    {
        // To do: throw exception if target list is empty.
    }

    public void AddTarget(Target target)
    {
        targets.Add(target);
    }

    public void RemoveTarget(Target target)
    {
        int targetIndex = targets.IndexOf(target);

        targets.RemoveAt(targetIndex);

        if (targets.Count == 0)
            LastTargetKilledEvent();
    }
}
