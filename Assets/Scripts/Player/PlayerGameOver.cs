using System;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    [NonSerialized] public bool isDead;
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
            GameOver();
    }
    private void GameOver()
    {
        GameOverWhitoutAnimation();
        GetComponent<Animator>().SetTrigger("deathTrigger");

    }
    private void GameOverWhitoutAnimation()
    {
        isDead = true;
        GameManager.Instance.GameOverMenu();
        GameManager.Instance.IsOverSetTrue();
    }
}
