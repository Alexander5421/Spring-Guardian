using System;
using UnityEngine;

// responsible for each spawn point
public class Spawner:MonoBehaviour
{
    public SpawnManager spawnManager;
    // two essential arrays
    // enemy you want to spawn
    public int pathIndex;
    public int[] enemy;
    // spawn interval time array
    public float[] spawnInterval;

    private float timer;
    // the next enemy to spawn
    private int index;

    public void Start()
    {

        if (spawnManager == null)
        {
            Debug.LogError("SpawnManager not found");
            return;
        }
        
        if (enemy.Length != spawnInterval.Length)
        {
            Debug.LogError("Enemy and spawn interval array length must be the same!");
            return;
        }
        if (spawnInterval.Length == 0)
        {
            Debug.LogError("Spawn interval array length must be greater than 0!");
            return;
        }
        timer = spawnInterval[0];
        index = 0;
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        while (timer <= 0)
        {

            // spawn enemy
            SpawnEnemy(enemy[index], pathIndex);
            // increase index
            index++;
            // if index is out of range, disable this spawner
            if (index >= enemy.Length)
            {
                enabled = false;
                return;
            }
            // reset timer
            timer = spawnInterval[index];
        }
    }

    private void SpawnEnemy(int enemyIndex, int pathIndex)
    {
        spawnManager.SpawnEnemy(enemyIndex,pathIndex);
    }
}
