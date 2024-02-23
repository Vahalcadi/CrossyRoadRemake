using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [NonSerialized] public int numberOfSteps;
    public int moveOnZ;
    public int moveOnX;
    [SerializeField] private MeshRenderer playerMesh;
    private Animator anim;
    public bool hasMoved;
    public bool isHopping;
    [SerializeField] int playerMovementXOnLogs;

    [SerializeField] private LayerMask isWall;

    private bool canMoveForward;
    private bool canMoveBackwards;
    private bool canMoveLeft;
    private bool canMoveRight;

    private float logDir;
    [NonSerialized] public bool canMove = true;
    //private bool walkOnLog;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        canMoveForward = !Physics.Raycast(transform.position, Vector3.forward, moveOnZ, isWall); 
        canMoveBackwards = !Physics.Raycast(transform.position, Vector3.back, moveOnZ, isWall); 
        canMoveLeft = !Physics.Raycast(transform.position, Vector3.left, moveOnX, isWall); 
        canMoveRight = !Physics.Raycast(transform.position, Vector3.right, moveOnX, isWall);

        if (!canMoveForward || !canMoveBackwards || !canMoveLeft || !canMoveRight)
            transform.parent = null;

        Move();
    }

    
    private void Move()
    {
        if (!canMove || GameManager.Instance.GetIsOver())
            return;

        if (Input.GetKeyUp(KeyCode.UpArrow) && !isHopping && canMoveForward)
        {
            transform.parent = null;

            numberOfSteps = 0;

            GameManager.Instance.CanSpawnTerrain();

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z + 5));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveOnZ);
            hasMoved = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && !isHopping && canMoveLeft)
        {
            numberOfSteps = 0;

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x - 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x - moveOnX, transform.position.y, transform.position.z);
            hasMoved = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && !isHopping && canMoveBackwards)
        {
            numberOfSteps++;

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z - 5));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveOnZ);
            hasMoved = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && !isHopping && canMoveRight)
        {
            numberOfSteps = 0;

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x + 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x + moveOnX, transform.position.y, transform.position.z);
            hasMoved = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Log"))
        {
            Debug.Log("enter");
            transform.parent = collision.gameObject.transform;   
            transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z);
        }     
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("exit");
        transform.parent = null;
    }

    public void EndHop() => isHopping = false;
}
