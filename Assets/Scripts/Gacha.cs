using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    [SerializeField] private List<Mesh> skins;
    public List<Mesh> unlockedSkins;
    [SerializeField] private GameObject player;
    private int random;
    public static Gacha Instance;


    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    public void EquipRandomSkin()
    {
        player.GetComponentInChildren<MeshFilter>().mesh = unlockedSkins[Random.Range(0, unlockedSkins.Count)];
    }

    public void UnlockSkin()
    {
        if (GameManager.Instance.coinCount < 100)
            return;

        GameManager.Instance.coinCount -= 100;

        random = Random.Range(0, skins.Count);

        Mesh mesh = skins[random];

        unlockedSkins.Add(mesh);

        skins.Remove(mesh);
    }
}
