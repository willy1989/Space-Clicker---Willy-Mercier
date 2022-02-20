using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveIndexPersistentData
{
    public int WaveIndex;

    public WaveIndexPersistentData(int _waveIndex)
    {
        WaveIndex = _waveIndex;
    }
}
