using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [Header("When this is useless..")]
    [SerializeField] private int destroyGameObject_When_X = -40;

    void Update()
    {
        transform.Translate(Vector3.forward * Random.Range(minSpeed, maxSpeed) * Time.deltaTime);
        if (transform.position.x <= destroyGameObject_When_X)
        {
            Destroy(gameObject);
        }
    }
}
