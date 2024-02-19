using System.Collections;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private bool facingLeft;

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
            GameObject obj = Instantiate(vehicle, spawnpoint.position, vehicle.transform.rotation);
            if (!facingLeft)
                obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}
