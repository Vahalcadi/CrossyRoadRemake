using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    [SerializeField] private List<Mesh> skins;
    public List<Mesh> unlockedSkins;

    private int random;
    public static Gacha Instance;


    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    public void UnlockSkin()
    {
        GameManager.Instance.coinCount -= 100;

        random = Random.Range(0, skins.Count);

        Mesh mesh = skins[random];

        unlockedSkins.Add(mesh);

        skins.Remove(mesh);
    }
}
