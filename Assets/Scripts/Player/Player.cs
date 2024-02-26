using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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

    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsGround;

    private bool canMoveForward;
    private bool canMoveBackwards;
    private bool canMoveLeft;
    private bool canMoveRight;
    private bool canJump;

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
        canMoveForward = !Physics.Raycast(transform.position, Vector3.forward, moveOnZ, whatIsWall); 
        canMoveBackwards = !Physics.Raycast(transform.position, Vector3.back, moveOnZ, whatIsWall); 
        canMoveLeft = !Physics.Raycast(transform.position, Vector3.left, moveOnX, whatIsWall); 
        canMoveRight = !Physics.Raycast(transform.position, Vector3.right, moveOnX, whatIsWall);
        canJump = Physics.Raycast(transform.position, Vector3.down, moveOnZ, whatIsGround);

        if (!canMoveForward || !canMoveBackwards || !canMoveLeft || !canMoveRight)
            transform.parent = null;

        Move();
    }

    
    private void Move()
    {
        if (!canMove || GameManager.Instance.GetIsOver())
            return;

        if (Input.GetKeyUp(KeyCode.UpArrow) && !isHopping && canMoveForward && canJump)
        {
            if (transform.parent != null)
                transform.parent = null;

            numberOfSteps = 0;

            GameManager.Instance.CanSpawnTerrain();

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z + 5));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z + moveOnZ);
            hasMoved = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && !isHopping && canMoveLeft && canJump)
        {
            numberOfSteps = 0;

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x - 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x - moveOnX), transform.position.y, transform.position.z);
            hasMoved = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && !isHopping && canMoveBackwards && canJump)
        {
            numberOfSteps++;

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z - 5));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z - moveOnZ);
            hasMoved = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && !isHopping && canMoveRight && canJump)
        {
            numberOfSteps = 0;

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x + 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x + moveOnX), transform.position.y, transform.position.z);
            hasMoved = true;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Log"))
        {
            Debug.Log("enter");
            transform.parent = collision.gameObject.transform;   
            transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z);
        }     
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Log"))
        {
            transform.parent = other.gameObject.transform;
            transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - moveOnZ, transform.position.z));
    }

    public void EndHop() => isHopping = false;
}
