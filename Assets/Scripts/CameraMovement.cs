using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Player player;
    [SerializeField] private float speed;

    [SerializeField] private float cameraScrollingOnPlayerMovement;

    [Header("When player is death")]
    [SerializeField] KillerDrone killerDrone;

    private bool activeCoroutine;

    private void Awake()
    {
        offset = transform.position - playerTransform.position;
    }
    private void Update()
    {
        if (player != null && player.hasMoved)
        {
            StartCoroutine(Move());
        }
        if (killerDrone.cameraOnPlayer)
        {
            StopCoroutine(Move());
            transform.parent = playerTransform;
            transform.position = new Vector3((killerDrone.transform.position.x + 2), (killerDrone.transform.position.y + 5), (killerDrone.transform.position.z - 5));
        }
    }

    private IEnumerator Move()
    {
        if (!activeCoroutine)
        {
            while (!player.gameObject.GetComponent<PlayerGameOver>().isDead)
            {
                yield return null;
                activeCoroutine = true;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Max(playerTransform.position.z - cameraScrollingOnPlayerMovement, transform.position.z + speed * Time.deltaTime));
            }
        }
    }

}
