using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("PauseMenu")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject buttonRestart;
    [SerializeField] GameObject buttonResume;

    [Header("UI")]
    public GameObject uiEscInfo;
    [SerializeField] GameObject buttonPause;

    [SerializeField] Transform newCamera;

    [Header("Score")]
    [SerializeField] GameObject scoreShowGameObject;
    [SerializeField] GameObject highScoreShowGameObject;
    [SerializeField] TextMeshProUGUI scoreShow;
    [SerializeField] TextMeshProUGUI highScoreShow;
    [SerializeField] GameObject buttonResetHighScore;

    private int highScore;
    private int score;

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
        highScoreShowGameObject.SetActive(false);
        scoreShowGameObject.SetActive(false);
        if (Instance != null)
            Destroy(Instance.gameObject);

        else
            Instance = this;
    }
    //PlayerPrefs.SetInt("HighScore", highScore)
    void Start()
    {
        UiConfigAtStart();
        //isPaused = false;
        SavedHighScore();

        for (int i = 0; i < InitialTerrainCount; i++)
        {
            SpawnTerrain();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            scoreShowGameObject.SetActive(true);
        }
        if (player.transform.position.z > score - 3)
        {
            score++;
            scoreShow.text = score.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && canSpawnTerrain && !isOver)
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
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

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
            score = 0;
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
            pauseMenu.SetActive(false);
            buttonRestart.SetActive(true);
            buttonResume.SetActive(true);
            buttonResetHighScore.SetActive(true);

            uiEscInfo.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
            buttonRestart.SetActive(false);
            buttonResume.SetActive(false);
            buttonResetHighScore.SetActive(false);

            uiEscInfo.SetActive(true);
        }
    }
    public void Pause()
    {
        if (!isOver)
        {
            isPaused = !isPaused;
            ResuneMenu();
            UpdateGamePause();
        }
    }
    public void GameOverMenu()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        highScoreShow.text = "TOP " + highScore.ToString();

        buttonRestart.SetActive(true);
        uiEscInfo.SetActive(false);
    }
    public void IsOverSetTrue()
    {

        highScoreShowGameObject.SetActive(true);
        isOver = true;
    }
    public bool GetIsOver()
    {
        return isOver;
    }
    private void SavedHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
            highScore = PlayerPrefs.GetInt("HighScore");
        else
            highScore = 0;
    }
    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
        Debug.Log(highScore);
    }
    public void UiConfigAtStart()
    {
        //buttonPause.SetActive(true);
        uiEscInfo.SetActive(true);
    }
}
