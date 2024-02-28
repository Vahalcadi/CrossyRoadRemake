using System.Collections;
using UnityEngine;

public class KillerDrone : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float maxTimeTillDeathTimer;
    [SerializeField] private float speed;
    public bool cameraOnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetDeathTimer(maxTimeTillDeathTimer);
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.SetDeathTimer(GameManager.Instance.GetDeathTimer() - Time.deltaTime);

        if (player.numberOfSteps >= 4 || (GameManager.Instance.GetDeathTimer() <= 0 && player.hasMoved))
        {
            player.canMove = false;
            player.gameObject.GetComponent<PlayerGameOver>().isDead = true;
            transform.LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraOnPlayer = true;
            StartCoroutine(KillPlayer());
        }
    }

    private IEnumerator KillPlayer()
    {

        yield return new WaitForSeconds(.5f);
        GameManager.Instance.UpdateGamePause();
        GameManager.Instance.GameOverMenu();
        GameManager.Instance.IsOverSetTrue();
        player.gameObject.GetComponent<PlayerGameOver>().isDead = false;
        Time.timeScale = 0;
    }
}
