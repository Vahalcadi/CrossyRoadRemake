using System;
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

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            anim.SetBool("isPreparingHop", true);

        if (Input.GetKeyUp(KeyCode.UpArrow) && !isHopping && canMoveForward && canJump)
        {
            anim.SetBool("isPreparingHop", false);

            GameManager.Instance.SetDeathTimer(7);

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
            anim.SetBool("isPreparingHop", false);

            GameManager.Instance.SetDeathTimer(7);

            numberOfSteps = 0;

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x - 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x - moveOnX), transform.position.y, transform.position.z);
            hasMoved = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && !isHopping && canMoveBackwards && canJump)
        {
            anim.SetBool("isPreparingHop", false);
            GameManager.Instance.SetDeathTimer(7);

            numberOfSteps++;

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z - 5));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z - moveOnZ);
            hasMoved = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && !isHopping && canMoveRight && canJump)
        {
            anim.SetBool("isPreparingHop", false);

            GameManager.Instance.SetDeathTimer(7);

            numberOfSteps = 0;

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x + 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

            
            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x + moveOnX), transform.position.y, transform.position.z);
            hasMoved = true;
        }
    }

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
