using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Target))]
public class ChildTargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] childTargetPrefab;

    private float spawnDistance = 0.25f;

    private void Awake()
    {
        GetComponent<Target>().DeathEvent += SpawnChildTarget;
    }

    private void SpawnChildTarget()
    {
        foreach(GameObject childTarget in childTargetPrefab)
        {
            Vector2 spawnPosition = transform.position + (Vector3)Utils.GetRandomDirection() * spawnDistance;

            GameObject target = Instantiate(childTarget, spawnPosition, Quaternion.identity, transform.parent);

            WaveManager.Instance.CurrentWave.AddTarget(target.GetComponent<Target>());
        }
    }

    
}
