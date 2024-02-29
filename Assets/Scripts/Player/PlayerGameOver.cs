using System;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    [NonSerialized] public bool isDead;
    public GameObject killerDrone;
    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.gameObject.CompareTag("Vehicle"))
            GameOver();

        if (other.CompareTag("RiverWalls"))
        {
            GameOverWhitoutAnimation();
            isDead = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Water"))
        {
            GameOver();
            gameObject.transform.parent = null;
            gameObject.SetActive(false);
        }
    }
    private void GameOver()
    {
        GameOverWhitoutAnimation();
        GetComponent<Animator>().SetTrigger("deathTrigger");

    }
    private void GameOverWhitoutAnimation()
    {
        killerDrone.SetActive(false);
        isDead = true;
        GameManager.Instance.GameOverMenu();
        GameManager.Instance.IsOverSetTrue();
    }
}
