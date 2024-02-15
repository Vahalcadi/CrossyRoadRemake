using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            Instantiate(vehicle, spawnpoint.position, vehicle.transform.rotation);
        }
    }
}
