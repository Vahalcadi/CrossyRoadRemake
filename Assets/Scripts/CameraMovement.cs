using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Player player;
    [SerializeField] private float speed;

    [SerializeField] private float cameraScrollingOnPlayerMovement;

    [Header("When player is death")]
    [SerializeField] KillerDrone killerDrone;
    private bool isGrabbed = true;

    private PlayerGameOver playerGameOver;
    private bool activeCoroutine;

    private void Start()
    {
        playerGameOver = player.gameObject.GetComponent<PlayerGameOver>();
    }
    private void Update()
    {
        if (player != null && player.hasMoved && !GameManager.Instance.GetIsOver())
        {
            StartCoroutine(Move());
        }
        else if (killerDrone.transform.position.x <= 4 && isGrabbed == true)
        {
            transform.position = new Vector3(2, player.transform.position.y + 5, player.transform.position.z - 5);
            StopCoroutine(Move());
            //transform.parent = playerTransform;
            isGrabbed = false;
        }
    }

    private IEnumerator Move()
    {
        if (!activeCoroutine)
        {
            while (!playerGameOver.isDead)
            {
                yield return null;
                activeCoroutine = true;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Max(playerTransform.position.z - cameraScrollingOnPlayerMovement, transform.position.z + speed * Time.deltaTime));
            }
        }
    }

}
