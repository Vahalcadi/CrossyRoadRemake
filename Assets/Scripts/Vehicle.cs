using UnityEngine;

public enum VehicleType
{
    Log,
    Car,
    Train
}

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    public VehicleType type;
    private float currentSpeed;
    [Header("When this is useless..")]
    [SerializeField] private int destroyGameObject_When_X;

    private void Start()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        if (transform.position.x <= destroyGameObject_When_X || transform.position.x >= -destroyGameObject_When_X)
        {
            Destroy(gameObject);
        }
    }

}
