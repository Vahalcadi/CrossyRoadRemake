using UnityEngine;

public class CoinsCollectable : MonoBehaviour
{
    [SerializeField] private int spawnChance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectCoin();
            Destroy(gameObject);
        }
    }
}
