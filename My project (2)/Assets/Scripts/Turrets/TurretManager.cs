using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private List<TurretSpawner> turretSpawners;
    [SerializeField] private List<GameObject> turretPrefabs;

    private void Awake()
    {
        foreach (var spawner in turretSpawners)
            spawner.SetTurretPrefabs(turretPrefabs);
    }
}
