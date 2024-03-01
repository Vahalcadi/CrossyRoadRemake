using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Gacha : MonoBehaviour
{
    public List<Mesh> skins;
    public List<Mesh> unlockedSkins;
    public Animator anim;
    [SerializeField] private GameObject player;
    private int random;
    public static Gacha Instance;
    private int coins;

    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {

        coins = PlayerPrefs.GetInt("CoinsCollected");
        Debug.Log(coins);

        //int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        int unlockedSkinsCount = PlayerPrefs.GetInt("unlockedSkins");

        if (unlockedSkinsCount == 1)
            return;

        for (int i = 1; i < unlockedSkinsCount; i++)
        {
            unlockedSkins.Add(skins[i]);
        }
        
        //player.GetComponentInChildren<MeshFilter>().mesh = skins[selectedCharacter];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            ButtonPress();
        if (Input.GetKeyDown(KeyCode.K))
            coins = 100;

    }
    public void ButtonPress()
    {
        Debug.Log("anim");
        anim.SetTrigger("ButtonPress");
    }

    public void UnlockSkin()
    {
        if (coins < 100 || unlockedSkins.Count >= skins.Count)
            return;

        coins -= 100;
        PlayerPrefs.SetInt("CoinsCollected", coins);

        random = Random.Range(1, skins.Count);

        Mesh mesh = skins[random];
        unlockedSkins.Add(mesh);
        Debug.Log("giving skin");

        Instantiate(player, new Vector3(0,1, -1.71f), player.transform.rotation);
        player.GetComponent<MeshFilter>().mesh = mesh;
    }

  
}
