using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private float currentSpeed;
    [Header("When this is useless..")]
    [SerializeField] private int destroyGameObject_When_X = -40;

    private void Start()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        if (transform.position.x <= destroyGameObject_When_X)
        {
            Destroy(gameObject);
        }
    }
}
