using System.Collections;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private bool facingLeft;

    [SerializeField] private Animator trainSignalAnimator;
 
    // Start is called before the first frame update
    void Start()
    {
        if(vehicle.GetComponent<Vehicle>().type == VehicleType.Train)
            StartCoroutine(SpawnTrain());
        else if(vehicle.GetComponent<Vehicle>().type == VehicleType.Log)
            StartCoroutine(SpawnLog());
        else
            StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnLog()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            VehicleGenerator();
        }
    }

    private IEnumerator SpawnTrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            //--- Train signal ---//

            trainSignalAnimator.SetBool("IncomingTrain", true);

            yield return new WaitForSeconds(1.2f);

            VehicleGenerator();

            yield return new WaitForSeconds(1);
            trainSignalAnimator.SetBool("IncomingTrain", false);
        }
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            VehicleGenerator();
        }
    }

    private void VehicleGenerator()
    {
        GameObject obj = Instantiate(vehicle, spawnpoint.position, vehicle.transform.rotation);
        if (!facingLeft)
            obj.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
