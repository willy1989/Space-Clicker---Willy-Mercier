using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave row data", order = 1)]
public class WaveRowData : ScriptableObject
{
    [SerializeField] private GameObject rowPrefab;

    [SerializeField] private float cost;
}
