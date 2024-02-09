using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Vector3 currentPos = new Vector3(0, 0, 0);

    [SerializeField] private Player player;

    private bool canSpawnTerrain;

    private List<GameObject> gameObjects = new();
    private int count;
    public int terrainLimit;
    public int InitialTerrainCount;
    public List<TerrainScriptableObj> terrains = new();


    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {

        if (Instance != null)
            Destroy(Instance.gameObject);
        
        else
            Instance = this;


        for(int i = 0; i < InitialTerrainCount; i++)
        {
            SpawnTerrain();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canSpawnTerrain)
        {
            canSpawnTerrain = false;

            SpawnTerrain();
            if (count >= terrainLimit)
            {
                Destroy(gameObjects[0].gameObject);
                gameObjects.RemoveAt(0);
            }

            count++;
        }
    }

    private void SpawnTerrain()
    {
        gameObjects.Add(Instantiate(terrains[Random.Range(0, terrains.Count)].terrain, currentPos, Quaternion.identity));
        
        currentPos.z++;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void CanSpawnTerrain() => canSpawnTerrain = true;
}
