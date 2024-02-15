using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    void Update()
    {
        transform.Translate(Vector3.forward * Random.Range(minSpeed,maxSpeed) * Time.deltaTime);  
    }
}
