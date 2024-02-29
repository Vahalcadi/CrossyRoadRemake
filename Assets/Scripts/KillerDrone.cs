using System.Collections;
using UnityEngine;

public class KillerDrone : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float maxTimeTillDeathTimer;
    [SerializeField] private float speed;
    public bool killPlayer;
    private bool playerDrag;

    private PlayerGameOver playerGameOver;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetDeathTimer(maxTimeTillDeathTimer);
        playerGameOver = player.gameObject.GetComponent<PlayerGameOver>();
    }

    // Update is called once per frame
    void Update()
    {
    

        if (Input.GetKeyUp(KeyCode.UpArrow) && !GameManager.Instance.GetIsOver())
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }

        GameManager.Instance.SetDeathTimer(GameManager.Instance.GetDeathTimer() - Time.deltaTime);

        if (playerDrag == false && (player.numberOfSteps >= 4 || player.transform.position.z - Camera.main.transform.position.z <= 5 || (GameManager.Instance.GetDeathTimer() <= 0 && player.hasMoved)))
        {
            player.canMove = false;
            playerGameOver.isDead = true;
            transform.LookAt(player.transform.position);

            killPlayer = true;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        if (playerDrag == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-8, 6, transform.position.z), speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(KillPlayer());
            DragPlayer();
        }
    }

    public IEnumerator KillPlayer()
    {
        playerGameOver.isDead = false;
        yield return new WaitForSeconds(.5f);
        GameManager.Instance.IsOverSetTrue();
        GameManager.Instance.GameOverMenu();
    }
    private void DragPlayer()
    {
        player.transform.parent = transform;
        player.gameObject.GetComponent<BoxCollider>().enabled = false;
        player.gameObject.GetComponent<Rigidbody>().useGravity = false;
        playerDrag = true;
    }
    public bool GetPlayerDrag()
    {
        return playerDrag;
    }
}
