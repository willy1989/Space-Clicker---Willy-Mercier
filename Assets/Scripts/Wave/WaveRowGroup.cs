using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WaveRowGroup
{
    [SerializeField] private WaveRowPrefab[] waveRows;

    [SerializeField] private int cost;

    public int Cost { get { return cost; } }

    public WaveRowPrefab GetRandomRow()
    {
        int randomNumber = Utils.GetRandomNumber(startInclusive: 0, endExclusive: waveRows.Length);

        return waveRows[randomNumber];
    }
}
