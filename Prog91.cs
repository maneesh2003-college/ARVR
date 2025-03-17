using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevel = 1;
    public int totalLevels = 2;
    public int playerScore = 0;
    public int chancesRemaining = 3;
    public float timeRemaining = 60.0f;

    private TextMeshProUGUI scoreTextTMP;
    private TextMeshProUGUI levelTextTMP;
    private TextMeshProUGUI chancesTextTMP;
    private TextMeshProUGUI timeTextTMP;

    private bool gameActive = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to the scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // This will be called every time a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the UI references and find new ones
        scoreTextTMP = null;
        levelTextTMP = null;
        chancesTextTMP = null;
        timeTextTMP = null;

        // Find new UI elements in the new scene
        FindTextElements();
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the GameManager is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        FindTextElements();
    }

    void FindTextElements()
    {
        // Find all TextMeshPro elements in the scene
        TextMeshProUGUI[] allTextElements = FindObjectsOfType<TextMeshProUGUI>(true);

        foreach (TextMeshProUGUI textElement in allTextElements)
        {
            string text = textElement.text.ToLower();

            if (text.Contains("score"))
                scoreTextTMP = textElement;
            else if (text.Contains("level"))
                levelTextTMP = textElement;
            else if (text.Contains("chances") || text.Contains("chance"))
                chancesTextTMP = textElement;
            else if (text.Contains("time"))
                timeTextTMP = textElement;
        }

        // Update the UI right away
        UpdateUI();

        Debug.Log("UI Elements found: " +
                  "Score: " + (scoreTextTMP != null) + ", " +
                  "Level: " + (levelTextTMP != null) + ", " +
                  "Chances: " + (chancesTextTMP != null) + ", " +
                  "Time: " + (timeTextTMP != null));
    }

    void Update()
    {
        if (gameActive)
        {
            UpdateTimer();
            UpdateUI();

            // If we're missing any UI elements, try to find them again
            if (scoreTextTMP == null || levelTextTMP == null ||
                chancesTextTMP == null || timeTextTMP == null)
            {
                FindTextElements();
            }
        }
    }

    void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            LoseChance();
        }
    }

    void UpdateUI()
    {
        if (scoreTextTMP != null)
            scoreTextTMP.text = "Score: " + playerScore;

        if (levelTextTMP != null)
            levelTextTMP.text = "Level: " + currentLevel + "/" + totalLevels;

        if (chancesTextTMP != null)
            chancesTextTMP.text = "Chances: " + chancesRemaining;

        if (timeTextTMP != null)
            timeTextTMP.text = "Time: " + Mathf.Round(timeRemaining);
    }

    public void AddScore(int points)
    {
        playerScore += points;
        Debug.Log("Score added: " + points + ", New score: " + playerScore);
    }

    public void LoseChance()
    {
        chancesRemaining--;
        Debug.Log("Lost a chance. Remaining: " + chancesRemaining);

        if (chancesRemaining <= 0)
        {
            GameOver();
        }
        else
        {
            // Reset the current level
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        timeRemaining = 60.0f;
        // Reload the current scene to reset the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CompleteLevel()
    {
        currentLevel++;
        Debug.Log("Level completed. Moving to level: " + currentLevel);

        if (currentLevel > totalLevels)
        {
            // Game completed
            Victory();
        }
        else
        {
            // Load next level
            timeRemaining = 60.0f;

            // Load the next scene if you have multiple scenes
            if (SceneManager.sceneCountInBuildSettings > currentLevel)
            {
                SceneManager.LoadScene(currentLevel - 1);  // Assuming scenes are 0-indexed
            }
            else
            {
                // If you don't have multiple scenes, just reload the current scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void GameOver()
    {
        gameActive = false;
        Debug.Log("Game Over!");
        // In a real game, you might show a game over screen here
    }

    void Victory()
    {
        gameActive = false;
        Debug.Log("Victory! You completed all levels!");
        // In a real game, you might show a victory screen here
    }
}