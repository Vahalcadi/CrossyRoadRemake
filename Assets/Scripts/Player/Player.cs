using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveOnZ;
    public int moveOnX;
    [SerializeField] private MeshRenderer playerMesh;
    private Animator anim;
    public bool hasMoved;
    public bool isHopping;

    private bool WalkOnLog;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Log"))
            WalkOnLog = true;
        if (!other.CompareTag("Log"))
            WalkOnLog = false;
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
    private void OnLog()
    {
        if (WalkOnLog)
            transform.position = new Vector3(transform.position.x - 4 * Time.deltaTime, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void EndHop() => isHopping = false;
}
