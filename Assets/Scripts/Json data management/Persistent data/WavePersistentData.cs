using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WavePersistentData
{
    public int WaveIndex;

    public float Budget;

    public WavePersistentData(int _WaveIndex, float _Budget)
    {
        WaveIndex = _WaveIndex;

        Budget = _Budget;
    }
}
