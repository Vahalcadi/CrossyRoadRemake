using System;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    [NonSerialized] public bool isDead;
    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.gameObject.CompareTag("Vehicle"))
            GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Water"))
            GameOver();
    }
    private void GameOver()
    {
        GetComponent<Animator>().SetTrigger("deathTrigger");
        GameManager.Instance.UpdateGamePause();
        GameManager.Instance.GameOverMenu();
        GameManager.Instance.IsOverSetTrue();
        Time.timeScale = 0;
    }
}
