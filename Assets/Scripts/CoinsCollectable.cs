using UnityEngine;

public class CoinsCollectable : MonoBehaviour
{
    public int spawnChance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectCoin();
            Destroy(gameObject);
        }
    }
}
