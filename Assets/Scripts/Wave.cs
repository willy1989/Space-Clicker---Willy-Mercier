using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private List<Target> targets;

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
        targets = GetComponentsInChildren<Target>().ToList();
    }

    public void AddTarget(Target target)
    {
        targets.Add(target);
    }

    public void RemoveTarget(Target target)
    {
        int targetIndex = targets.IndexOf(target);

        if(targetIndex != -1)
            targets.RemoveAt(targetIndex);

        if (targets.Count == 0 && LastTargetKilledEvent != null)
            LastTargetKilledEvent();
    }
}
