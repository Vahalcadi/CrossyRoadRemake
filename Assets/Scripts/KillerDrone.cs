using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class KillerDrone : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float deathTimer;
    [SerializeField] private float speed;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = deathTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (player.numberOfSteps >= 3 || (timer <= 0 && !player.hasMoved))
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
            StartCoroutine(KillPlayer());
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
