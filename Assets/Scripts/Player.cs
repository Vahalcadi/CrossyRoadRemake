using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveOnZ;
    public int moveOnX;
    [SerializeField] private MeshRenderer playerMesh;
    private Animator anim;
    public bool hasMoved;
    public bool isHopping;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {       
        if (Input.GetKeyDown(KeyCode.W) && !isHopping)
        {
            GameManager.Instance.CanSpawnTerrain();

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z + 5));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveOnZ);
            hasMoved = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isHopping)
        {
            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(transform.position.x - 5, transform.position.y, transform.position.z));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x - moveOnX, transform.position.y, transform.position.z);
            hasMoved = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isHopping)
        {
            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z - 5));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveOnZ);
            hasMoved = true;
        }

        else if (Input.GetKeyDown(KeyCode.D) && !isHopping)
        {
            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(transform.position.x + 5, transform.position.y, transform.position.z));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x + moveOnX, transform.position.y, transform.position.z);
            hasMoved = true;
        }
    }

    public void EndHop() => isHopping = false;
}
