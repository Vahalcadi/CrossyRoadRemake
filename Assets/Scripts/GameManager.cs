using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject restartButton;
    [SerializeField] Transform camera;

    private bool isPaused;
    private bool isOver;

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
        isPaused = false;

        if (Instance != null)
            Destroy(Instance.gameObject);

        else
            Instance = this;


        for (int i = 0; i < InitialTerrainCount; i++)
        {
            SpawnTerrain();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            ResuneMenu();
            UpdateGamePause();
        }

        if (Input.GetKeyUp(KeyCode.W) && canSpawnTerrain)
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
    private void LateUpdate()
    {
        GameOverConditions();
    }

    private void SpawnTerrain()
    {
        gameObjects.Add(Instantiate(terrains[Random.Range(0, terrains.Count)].terrain, currentPos, Quaternion.identity));

        currentPos.z++;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void CanSpawnTerrain() => canSpawnTerrain = true;
    private void GameOverConditions()
    {
        if (player.transform.position.z + 4 < camera.transform.position.z)
        {
            isOver = true;
            Time.timeScale = 0;
            GameOverMenu();
        }
    }
    public void UpdateGamePause()
    {
        if (isPaused && !isOver)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    private void ResuneMenu()
    {
        if (isPaused == true && !isOver)
        {
            pauseMenu.SetActive(true);
            resumeButton.SetActive(true);
            restartButton.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
            resumeButton.SetActive(false);
            restartButton.SetActive(false);
        }
    }
    private void GameOverMenu()
    {

    }
}
