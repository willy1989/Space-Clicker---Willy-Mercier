using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveRowPrefab : MonoBehaviour
{
    [HideInInspector] public Target[] Targets;

    private void Awake()
    {
        Targets = GetComponentsInChildren<Target>();
    }
}
