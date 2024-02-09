using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Player player;
    [SerializeField] private float speed;
   
    [SerializeField] private float cameraScrollingOnPlayerMovement = 6f; 
    
    private bool activeCoroutine;

    private void Update()
    {
        if (player.hasMoved)
        {
            StartCoroutine(Move());
            transform.position = new Vector3(player.transform.position.x + 2.5f, transform.position.y, transform.position.z);         
        }
    }

    private IEnumerator Move()
    {
        if (!activeCoroutine)
        {
            while (true)
            {
                yield return null;
                activeCoroutine = true;
                transform.position = new Vector3(transform.position.x, transform.position.y,Mathf.Max(playerTransform.position.z - cameraScrollingOnPlayerMovement, transform.position.z + speed * Time.deltaTime));
            }
        }
    }

}
