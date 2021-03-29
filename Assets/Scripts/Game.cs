using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public int lives;
    public int score;

    [SerializeField]
    private Player player;

    [SerializeField]
    private Transform playerSpawnPoint;

    [SerializeField]
    private Spawner[] spawners;

    [SerializeField]
    private int level;

    private string nextLevel;


    public void LoseLife()
    {
        if (lives > 0)
        {
            StartCoroutine(Respawn());
        }

        else
        {
            Debug.Log("Game Over");
        }
    }

    IEnumerator Respawn()
    {
        lives--;
        yield return new WaitForSeconds(2f);
        Instantiate(player.gameObject, playerSpawnPoint.position, Quaternion.identity);
    }

    public void AddPoints(int points)
    {
        score += points;

        CheckForLevelCompletion();
    }

    private void CheckForLevelCompletion()
    {
        if (!FindObjectOfType<Enemy>())
        {
            foreach (Spawner spawner in spawners)
            {
                if (spawner.completed)
                {
                    CompleteLevel();
                }
            }
        }
    }

    private void CompleteLevel()
    {
        nextLevel = "Assets/Scenes/Level " + (level + 1) + ".unity";

        if (SceneUtility.GetBuildIndexByScenePath(nextLevel) >= 0)
        {
            SceneManager.LoadSceneAsync(++level);
        }

        else
        {
            Debug.Log("Game Won!");
        }
    }
}
