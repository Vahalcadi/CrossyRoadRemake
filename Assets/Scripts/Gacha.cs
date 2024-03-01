using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private List<int> extractedIndexes = new List<int>();

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
        if (Input.GetKeyDown(KeyCode.C))
            PlayerPrefs.DeleteAll();

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

        CheckExtractedIndexes();


        unlockedSkins.Add(skins[random]);
        Debug.Log("giving skin");

        PlayerPrefs.SetInt("unlockedSkins", unlockedSkins.Count);

        Instantiate(player, new Vector3(0,1, -1.71f), player.transform.rotation);
        player.GetComponentInChildren<MeshFilter>().mesh = unlockedSkins.Last();
    }

    private void CheckExtractedIndexes()
    {
        random = Random.Range(1, skins.Count);

        if (extractedIndexes.Contains(random))
            CheckExtractedIndexes();
        else
            extractedIndexes.Add(random);
    }
}
