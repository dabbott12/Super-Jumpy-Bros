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

    public void LoseLife()
    {
        if (lives > 0)
        {
            StartCoroutine(Respawn());
        }

        else
        {

        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        lives--;
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
        level++;

        if (SceneManager.GetSceneAt(level) == null)
        {
            SceneManager.LoadScene(level);
        }

        else
        {
            Debug.Log("Game Won!");
        }
    }
}
