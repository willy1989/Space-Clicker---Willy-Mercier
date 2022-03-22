using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave
{
    private List<Target> targets = new List<Target>();

    public List<Target> Targets
    {
        get
        {
            return targets;
        }
    }

    public void AddTarget(Target newTarget)
    {
        targets.Add(newTarget);
    }

    public void AddTargets(Target[] newTartgets)
    {
        targets.AddRange(newTartgets);
    }

    public void RemoveTarget(Target target)
    {
        int targetIndex = targets.IndexOf(target);

        if(targetIndex != -1)
            targets.RemoveAt(targetIndex);
    }
}
