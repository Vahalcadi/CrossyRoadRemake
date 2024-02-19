using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("PauseMenu")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject buttonRestart;
    [SerializeField] GameObject buttonResume;
    [Header("UI")]
    [SerializeField] GameObject uiEscInfo;

    [SerializeField] Transform newCamera;

    private bool isPaused;
    private bool isOver;

    private Vector3 currentPos = new Vector3(0, 0, 0);

    [SerializeField] private Player player;

    private bool canSpawnTerrain;

    private List<GameObject> instantiatedTerrains = new();
    private int count;
    public int terrainLimit;
    public int InitialTerrainCount;
    public List<GameObject> terrains = new();
    private int random;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //isPaused = false;

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
            Pause();
        }

        if (Input.GetKeyUp(KeyCode.W) && canSpawnTerrain && !isOver)
        {
            Time.timeScale = 1;
            canSpawnTerrain = false;

            SpawnTerrain();
            if (count >= terrainLimit)
            {
                Destroy(instantiatedTerrains[0].gameObject);
                instantiatedTerrains.RemoveAt(0);
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
        random = Random.Range(0, terrains.Count);

        instantiatedTerrains.Add(Instantiate(terrains[random], currentPos + terrains[random].transform.position, Quaternion.identity));

        currentPos.z++;
    }

    public void RestartGame()
    {
        isOver = false;
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void CanSpawnTerrain() => canSpawnTerrain = true;
    private void GameOverConditions()
    {
        if (player.transform.position.z + 4 < newCamera.transform.position.z)
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
        if (isPaused && !isOver)
        {
            pauseMenu.SetActive(true);
            buttonRestart.SetActive(true);
            buttonResume.SetActive(true);

            uiEscInfo.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(false);
            buttonRestart.SetActive(false);
            buttonResume.SetActive(false);

            uiEscInfo.SetActive(true);
        }
    }
    public void Pause()
    {
        isPaused = !isPaused;
        ResuneMenu();
        UpdateGamePause();
    }
    public void GameOverMenu()
    {
        buttonRestart.SetActive(true);
        uiEscInfo.SetActive(false);
    }
    public void IsOverSetTrue()
    {
        isOver = true;
    }
    public bool GetIsOver()
    {
        return isOver;
    }
}
