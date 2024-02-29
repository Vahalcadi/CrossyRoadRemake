using System.Collections;
using UnityEngine;

public class KillerDrone : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float maxTimeTillDeathTimer;
    [SerializeField] private float speed;
    public bool killPlayer;

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

        if (!playerGameOver.isDead && player.numberOfSteps >= 4 || (GameManager.Instance.GetDeathTimer() <= 0 && player.hasMoved))
        {
            player.canMove = false;
            playerGameOver.isDead = true;
            transform.LookAt(player.transform.position);
            
            killPlayer = true;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
}
