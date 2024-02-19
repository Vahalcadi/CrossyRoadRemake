using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle") || collision.gameObject.CompareTag("Water"))
        {
            GameManager.Instance.UpdateGamePause();
            GameManager.Instance.GameOverMenu();
            GameManager.Instance.IsOverSetTrue();
            Time.timeScale = 0;
        }
    }
}
