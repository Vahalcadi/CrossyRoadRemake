using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Veicle"))
        {
            gameManager.UpdateGamePause();
            gameManager.GameOverMenu();
            gameManager.IsOverSetTrue();
            Time.timeScale = 0;
        }
    }
}
