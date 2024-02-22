using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveOnZ;
    public int moveOnX;
    [SerializeField] private MeshRenderer playerMesh;
    private Animator anim;
    public bool hasMoved;
    public bool isHopping;

    private float logDir;

    private bool walkOnLog;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        OnLog();
    }

    
    private void Move()
    {
        if (GameManager.Instance.GetIsOver())
            return;

        if (Input.GetKeyUp(KeyCode.UpArrow) && !isHopping)
        {
            GameManager.Instance.CanSpawnTerrain();

            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z + 5));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveOnZ);
            hasMoved = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && !isHopping)
        {
            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x - 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x - moveOnX, transform.position.y, transform.position.z);
            hasMoved = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && !isHopping)
        {
            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z - 5));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveOnZ);
            hasMoved = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && !isHopping)
        {
            isHopping = true;

            playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x + 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

            anim.SetTrigger("hopTrigger");

            transform.position = new Vector3(transform.position.x + moveOnX, transform.position.y, transform.position.z);
            hasMoved = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        walkOnLog = false;
        if (collision.collider.CompareTag("Log"))
        {
            Debug.Log("enter");
            walkOnLog = true;
            logDir = collision.transform.forward.normalized.x;
        }
        
    }

    /*private void OnCollisionExit(Collision collision)
    {
        Debug.Log("exit");
        walkOnLog = false;
    }*/

    private void OnLog()
    {
        if (walkOnLog)
            transform.position = new Vector3(transform.position.x + 7 * logDir * Time.deltaTime, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void EndHop() => isHopping = false;
}
