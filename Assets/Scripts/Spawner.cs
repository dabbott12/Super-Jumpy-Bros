using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Enemy[] enemies;
    
    [SerializeField]
    private float spawnTimeDelay;

    [SerializeField]
    private float startSpawnDelay;
    
    public bool completed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startSpawnDelay);

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyInstance = Instantiate(enemies[i], transform.position, Quaternion.identity);
            
            if (i >= (enemies.Length - 1))
            {
                completed = true;
            }

            yield return new WaitForSeconds(spawnTimeDelay);
        }
    }
}
