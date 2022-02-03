using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Target))]
public class ChildTargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] childTargetPrefab;

    private Vector2 spaceBetweenSpawn = Vector2.right/3f;

    private void Awake()
    {
        GetComponent<Target>().DeathEvent += SpawnChildTarget;
    }

    private void SpawnChildTarget()
    {
        Vector2 spawnStartPosition = transform.position;

        foreach(GameObject childTarget in childTargetPrefab)
        {
            GameObject target = Instantiate(childTarget, spawnStartPosition, Quaternion.identity);

            WaveManager.Instance.currentWave.AddTarget(target.GetComponent<Target>());
            spawnStartPosition += spaceBetweenSpawn;
        }
    }
}
