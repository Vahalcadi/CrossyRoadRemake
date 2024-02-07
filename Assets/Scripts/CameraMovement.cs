using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float speed;
    private bool activeCoroutine;



    private void Update()
    {
        if (player.hasMoved)
        {
            StartCoroutine(Move());
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);         
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
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
            }
        }
    }

}
