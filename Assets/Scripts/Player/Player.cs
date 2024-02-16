using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveOnZ;
    public int moveOnX;
    [SerializeField] private MeshRenderer playerMesh;
    private Animator anim;
    public bool hasMoved;
    public bool isHopping;
    [SerializeField] private GameManager gameManager;

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
        if (!gameManager.GetIsOver())
        {
            if (Input.GetKeyUp(KeyCode.W) && !isHopping)
            {
                GameManager.Instance.CanSpawnTerrain();

                isHopping = true;

                playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z + 5));

                anim.SetTrigger("hopTrigger");

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveOnZ);
                hasMoved = true;
            }
            else if (Input.GetKeyUp(KeyCode.A) && !isHopping)
            {
                isHopping = true;

                playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x - 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

                anim.SetTrigger("hopTrigger");

                transform.position = new Vector3(transform.position.x - moveOnX, transform.position.y, transform.position.z);
                hasMoved = true;
            }
            else if (Input.GetKeyUp(KeyCode.S) && !isHopping)
            {
                isHopping = true;

                playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x, playerMesh.transform.position.y, playerMesh.transform.position.z - 5));

                anim.SetTrigger("hopTrigger");

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveOnZ);
                hasMoved = true;
            }
            else if (Input.GetKeyUp(KeyCode.D) && !isHopping)
            {
                isHopping = true;

                playerMesh.transform.LookAt(new Vector3(playerMesh.transform.position.x + 5, playerMesh.transform.position.y, playerMesh.transform.position.z));

                anim.SetTrigger("hopTrigger");

                transform.position = new Vector3(transform.position.x + moveOnX, transform.position.y, transform.position.z);
                hasMoved = true;
            }
        }
    }

    public void EndHop() => isHopping = false;
}
