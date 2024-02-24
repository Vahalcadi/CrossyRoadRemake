using UnityEngine;

public class CoinsCollectable : MonoBehaviour
{
    GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CoinPlusOne("imACoin");
            Destroy(gameObject);
        }
    }
}
