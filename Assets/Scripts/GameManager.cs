using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("PauseMenu")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject buttonRestart;
    [SerializeField] GameObject buttonResume;
    [SerializeField] Button buttonQuit;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI timerPause;
    [SerializeField] GameObject buttonAudioOn;
    [SerializeField] GameObject buttonAudioOff;
    private bool buttonAudioVisible;
    private bool volumeOn;
    [SerializeField] GameObject controlsInformations;
    [SerializeField] GameObject buttonCharacterSelection;
    [SerializeField] GameObject buttonPause;
    [SerializeField] TextMeshProUGUI coinsCollected;
    [SerializeField] private Button gachaButton;
    [NonSerialized] public int coinCount;

    private bool started;
    private bool onPauseMenu;

    [SerializeField] Transform newCamera;

    [Header("Score")]
    [SerializeField] GameObject scoreShowGameObject;
    [SerializeField] GameObject highScoreShowGameObject;
    [SerializeField] TextMeshProUGUI scoreShow;
    [SerializeField] TextMeshProUGUI highScoreShow;
    [SerializeField] GameObject buttonResetHighScore;
    [Header("Others")]

    private int highScore;
    private int score;

    private bool isPaused;
    private bool isOver;

    private Vector3 currentPos = new Vector3(0, 0, 4);

    [SerializeField] private Player player;

    private bool canSpawnTerrain;

    private List<GameObject> instantiatedTerrains = new();
    private int count;
    public int terrainLimit;
    public int InitialTerrainCount;
    public List<GameObject> terrains = new();
    [SerializeField] private GameObject coinPrefab;
    private int random;
    private float deathTimer;

    public static GameManager Instance;

    private void Awake()
    {
        gachaButton.gameObject.SetActive(false);
        coinCount = PlayerPrefs.GetInt("CoinsCollected");
        coinsCollected.text = coinCount.ToString();
        highScoreShowGameObject.SetActive(false);
        scoreShowGameObject.SetActive(false);
        PlayerPrefs.GetInt("volumeOn");
        buttonAudioVisible = true;
        ButtonAudioOnOff();
        PlayerPrefs.Save();

        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }
    //PlayerPrefs.SetInt("HighScore", highScore)
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        player.GetComponentInChildren<MeshFilter>().mesh = Gacha.Instance.skins[selectedCharacter];

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
        if (player.transform.position.z > score)
        {
            score++;
            scoreShow.text = score.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && started == true && onPauseMenu == false)
        {
            onPauseMenu = true;
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            coinCount = 100;
            PlayerPrefs.SetInt("CoinsCollected", coinCount);
            coinsCollected.text = $"{coinCount}";
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && canSpawnTerrain && !isOver)
        {
            controlsInformations.SetActive(false);
            buttonCharacterSelection.SetActive(false);
            buttonPause.SetActive(true);

            buttonAudioVisible = false;
            ButtonAudioOnOff();
            started = true;

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
        //if (player.transform.position.y <= -1)
        //{
        //    isOver = true;
        //    score = 0;
        //    Time.timeScale = 0;
        //    GameOverMenu();
        //}
    }
    private void LateUpdate()
    {
        GameOverConditions();
    }

    private void SpawnTerrain()
    {

        random = UnityEngine.Random.Range(0, terrains.Count);

        instantiatedTerrains.Add(Instantiate(terrains[random], currentPos + terrains[random].transform.position, terrains[random].transform.rotation));

        if (UnityEngine.Random.Range(0, 101) < coinPrefab.GetComponent<CoinsCollectable>().spawnChance && !terrains[random].CompareTag("Water"))
            Instantiate(coinPrefab, instantiatedTerrains.Last().transform.position + new Vector3(UnityEngine.Random.Range(-4, 5), 0.023f, 0), coinPrefab.transform.rotation);

        if (terrains[random].CompareTag("Rooftop"))
            currentPos.z = currentPos.z + 2;
        else
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

        SceneManager.LoadScene("MainScene");
    }

    public void CanSpawnTerrain() => canSpawnTerrain = true;
    private void GameOverConditions()
    {
        if (player.transform.position.z + 4 < newCamera.transform.position.z)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isOver = true;
        score = 0;
        GameOverMenu();
    }

    public void UpdateGamePause()
    {
        if (isPaused && !isOver)
        {
            Time.timeScale = 0;
        }
        else
        {
            StartCoroutine(TimerPause());
        }
    }
    private void ResuneMenu()
    {
        if (isPaused && !isOver)
        {
            pauseMenu.SetActive(false);
            buttonResume.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(true);
            buttonResume.SetActive(false);
        }
    }
    public void Pause()
    {
        if (!isOver && started == true)
        {

            isPaused = !isPaused;
            ResuneMenu();
            UpdateGamePause();
        }
        else if (buttonQuit.gameObject.activeSelf == false)
            buttonQuit.gameObject.SetActive(true);
        else
            buttonQuit.gameObject.SetActive(false);
    }
    public void GameOverMenu()
    {
        if (coinCount >= 100)
            gachaButton.gameObject.SetActive(true);

        CoinsSaveInformation();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        highScoreShow.text = "TOP " + highScore.ToString();

        buttonAudioVisible = true;
        ButtonAudioOnOff();
        buttonCharacterSelection.SetActive(true);
        buttonRestart.SetActive(true);
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

    public void CollectCoin()
    {
        coinCount++;
        coinsCollected.text = $"{coinCount}";
    }

    public void CoinsSaveInformation()
    {
        PlayerPrefs.SetInt("CoinsCollected", +coinCount);
        PlayerPrefs.Save();
        Debug.Log(coinCount);
    }

    public float GetDeathTimer() => deathTimer;
    public void SetDeathTimer(float value) => deathTimer = value;

    public void ButtonAudioOnOff()
    {
        if (buttonAudioVisible == true)
        {
            if (volumeOn == true)
            {
                PlayerPrefs.SetInt("volumeOn", 1);
                buttonAudioOff.SetActive(false);
                buttonAudioOn.SetActive(true);
                volumeOn = false;
            }
            else
            {
                PlayerPrefs.SetInt("volumeOn", 0);
                buttonAudioOff.SetActive(true);
                buttonAudioOn.SetActive(false);
                volumeOn = true;
            }
        }
        else
        {
            buttonAudioOff.SetActive(false);
            buttonAudioOn.SetActive(false);
        }
        PlayerPrefs.Save();
    }
    private IEnumerator TimerPause()
    {
        timerPause.gameObject.SetActive(true);
        timerPause.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        timerPause.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        timerPause.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        onPauseMenu = false;
        timerPause.gameObject.SetActive(false);

    }

    public void OpenSkinSelection()
    {
        PlayerPrefs.SetInt("unlockedSkins", Gacha.Instance.unlockedSkins.Count);
        SceneManager.LoadScene("CharacterSelection");
    }

    public void OpenGacha()
    {
        PlayerPrefs.SetInt("unlockedSkins", Gacha.Instance.unlockedSkins.Count);
        SceneManager.LoadScene("Gacha");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
