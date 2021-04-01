using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public int lives;
    public int score;
    public int highScore;

    [SerializeField]
    private Player player;

    [SerializeField]
    private Transform playerSpawnPoint;

    [SerializeField]
    private Spawner[] spawners;

    [SerializeField]
    private int level;

    private string nextLevel;

    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI highScoreText;

    [SerializeField]
    private List<Spawner> numberOfSpawnersCompleted;

    private void Start()
    { 
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Load();

        highScoreText.SetText("High score: " + highScore);

        UpdateHUD();
    }

    void EndGame()
    {
        NewGame();

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            StartCoroutine(Respawn());
        }

        else
        {
            EndGame();
            Debug.Log("Game Over");
        }
    }

    IEnumerator Respawn()
    {
        lives--;
        UpdateHUD();

        yield return new WaitForSeconds(2f);
        Instantiate(player.gameObject, playerSpawnPoint.position, Quaternion.identity);
    }

    public void AddPoints(int points)
    {
        score += points;

        UpdateHUD();

        CheckForLevelCompletion();
    }

    private void CheckForLevelCompletion()
    {
        if (!FindObjectOfType<Enemy>())
        {
            foreach (Spawner spawner in spawners)
            {
                if (!spawner.completed)
                {
                    return;
                }
            }

            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        level += 1;
        Save();
        nextLevel = "Assets/Scenes/Level " + level + ".unity";

        if (SceneUtility.GetBuildIndexByScenePath(nextLevel) >= 0)
        {
            SceneManager.LoadSceneAsync(level);
        }

        else
        {
            EndGame();
            Debug.Log("Game Won!");
        }
    }


    private void Save()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.SetInt("Level", level);

    }

    private void Load()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        lives = PlayerPrefs.GetInt("Lives", 3);
        level = PlayerPrefs.GetInt("Level", level);
    }

    private void NewGame()
    {
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.DeleteKey("Level");
       // load menu or something
        // level = 0;
       // SceneManager.LoadScene(level);
    }

    void UpdateHUD()
    {
        scoreText.SetText("Score: " + score);
        livesText.SetText("Lives: " + lives);
    }
}
