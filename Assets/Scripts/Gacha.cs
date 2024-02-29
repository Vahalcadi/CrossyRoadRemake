using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        if (unlockedSkins.Count == 0)
            return;

        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        int unlockedSkinsCount = PlayerPrefs.GetInt("unlockedSkins");

        for (int i = 0; i < unlockedSkinsCount; i++)
        {
            unlockedSkins.Add(skins[i]);
        }
        for (int i = 0; i < unlockedSkins.Count; i++)
        {
            skins.Remove(unlockedSkins[i]);
        }
        player.GetComponentInChildren<MeshFilter>().mesh = unlockedSkins[selectedCharacter];
    }

    public void OpenSkinSelection()
    {
        PlayerPrefs.SetInt("unlockedSkins", unlockedSkins.Count);
        SceneManager.LoadScene("CharacterSelection");
    }

    public void UnlockSkin()
    {
        if (GameManager.Instance.coinCount < 100 || skins.Count <= 0)
            return;

        GameManager.Instance.coinCount -= 100;

        random = Random.Range(0, skins.Count);

        Mesh mesh = skins[random];
        unlockedSkins.Add(mesh);
        skins.Remove(mesh);
    }
}
