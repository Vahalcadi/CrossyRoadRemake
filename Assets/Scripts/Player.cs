using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveOnZ;
    public int moveOnX;
    [SerializeField] private MeshRenderer playerMesh;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerMesh.transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z + 5));
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveOnZ);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            playerMesh.transform.LookAt(new Vector3(transform.position.x - 5, transform.position.y, transform.position.z));
            transform.position = new Vector3(transform.position.x - moveOnX, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerMesh.transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z - 5));
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveOnZ);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerMesh.transform.LookAt(new Vector3(transform.position.x + 5, transform.position.y, transform.position.z));
            transform.position = new Vector3(transform.position.x + moveOnX, transform.position.y, transform.position.z);
        }
    }
}
