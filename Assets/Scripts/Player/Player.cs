using System;
using System.Collections;
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
    [SerializeField] private bool canJump;

    private float logDir;
    [SerializeField] public bool canMove = true;
    //private bool walkOnLog;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(EnableMovement());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
            PlayerPrefs.DeleteAll();

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.coinCount = 100;
            Gacha.Instance.UnlockSkin();
        }

        canMoveForward = !Physics.Raycast(transform.position, Vector3.forward, moveOnZ, whatIsWall);
        canMoveBackwards = !Physics.Raycast(transform.position, Vector3.back, moveOnZ, whatIsWall);
        canMoveLeft = !Physics.Raycast(transform.position, Vector3.left, moveOnX, whatIsWall);
        canMoveRight = !Physics.Raycast(transform.position, Vector3.right, moveOnX, whatIsWall);
        canJump = Physics.Raycast(transform.position, Vector3.down, moveOnZ, whatIsGround);

        /*if (!canMoveForward || !canMoveBackwards || !canMoveLeft || !canMoveRight)
            transform.parent = null;*/

        Move();
    }


    private void Move()
    {
        
        if (!canMove || GameManager.Instance.GetIsOver())
            return;
        

        if
        (
            (
                ((Input.GetKey(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.UpArrow)) && canMoveForward)
                || ((Input.GetKey(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.DownArrow)) && canMoveBackwards)
                || ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) && canMoveLeft)
                || ((Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.RightArrow)) && canMoveRight)
            )
            && !isHopping
            && canJump
        )
        {
            if (Input.GetKey(KeyCode.UpArrow))
                playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z + 5));
            else if (Input.GetKey(KeyCode.DownArrow))
                playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z - 5));
            else if (Input.GetKey(KeyCode.LeftArrow))
                playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x - 5, playerMesh.transform.position.y, playerMesh.transform.position.z));
            else if (Input.GetKey(KeyCode.RightArrow))
                playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x + 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

            anim.SetBool("isPreparingHop", true);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && !isHopping && canMoveForward && canJump)
        {
            
            GameManager.Instance.SetDeathTimer(7);

            if (transform.parent != null)
                transform.parent = null;

            numberOfSteps = 0;

            GameManager.Instance.CanSpawnTerrain();

            isHopping = true;
            Debug.Log($"isHopping:{isHopping}");

            Jump();

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z + moveOnZ);
            hasMoved = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && !isHopping && canMoveLeft && canJump)
        {
            

            GameManager.Instance.SetDeathTimer(7);

            numberOfSteps = 0;

            isHopping = true;
            Debug.Log($"isHopping:{isHopping}");

            Jump();

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x - moveOnX), transform.position.y, transform.position.z);
            hasMoved = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && !isHopping && canMoveBackwards && canJump)
        {
            if (transform.parent != null)
                transform.parent = null;

           
            GameManager.Instance.SetDeathTimer(7);

            numberOfSteps++;

            isHopping = true;
            Debug.Log($"isHopping:{isHopping}");

            Jump();

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z - moveOnZ);
            hasMoved = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && !isHopping && canMoveRight && canJump)
        {

            GameManager.Instance.SetDeathTimer(7);

            numberOfSteps = 0;

            isHopping = true;
            Debug.Log($"isHopping:{isHopping}");
            Jump();

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

    private IEnumerator EnableMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(.5f);
        canMove = true;
        isHopping = false;
        Debug.Log($"isHopping:{isHopping}");
    }

    private void Jump()
    {
        anim.ResetTrigger("toIdle");

        anim.SetBool("isPreparingHop", false);
        Debug.Log("preparing hop false");
        anim.SetTrigger("hopTrigger");

        Debug.Log("hopTrigger");

        StartCoroutine(EndHop());

    }
    public IEnumerator EndHop()
    {
        yield return new WaitForSeconds(.121f);
        anim.SetTrigger("toIdle");
        yield return null;

        isHopping = false;
        
        Debug.Log($"isHopping:{isHopping}");
    }
}
