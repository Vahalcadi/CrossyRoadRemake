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
        GameManager.Instance.SetDeathTimer(GameManager.Instance.GetDeathTimer() - Time.deltaTime);

        if (playerDrag == false && (!playerGameOver.isDead && player.numberOfSteps >= 4 || (GameManager.Instance.GetDeathTimer() <= 0 && player.hasMoved)))
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
            DragPlayer();
            StartCoroutine(KillPlayer());
        }
    }

    public IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(.5f);
        GameManager.Instance.GameOverMenu();
        GameManager.Instance.IsOverSetTrue();
        playerGameOver.isDead = false;
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
