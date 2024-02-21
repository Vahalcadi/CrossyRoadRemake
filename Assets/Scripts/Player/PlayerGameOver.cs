using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
            GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Water"))
            GameOver();
    }
    private void GameOver()
    {
        GameManager.Instance.UpdateGamePause();
        GameManager.Instance.GameOverMenu();
        GameManager.Instance.IsOverSetTrue();
        Time.timeScale = 0;
    }
}
