using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public float speedRandomizer = 0.2f;

    public List<PathCreator> paths;
    
    public List<Enemy> enemyPrefabs;
    
    public List<Enemy> existingEnemies = new List<Enemy>();
    
    public List<Spawners> waveSpawners = new List<Spawners>();
    
    [Serializable]
    public class Spawners
    {
        public List<Spawner> list ;
    }

    
    private int currentWave = 0;

    // TODO determine the enemy orientation based on the path
    public void SpawnEnemy(int enemyIndex, int pathIndex)
    {
        Enemy enemyPrefab = enemyPrefabs[enemyIndex];
        GameObject enemyObject = Instantiate(enemyPrefab.gameObject, transform, true);
        enemyObject.transform.position = paths[pathIndex].path.GetPointAtDistance(0);
        Enemy enemyScript = enemyObject.GetComponent<Enemy>();
        existingEnemies.Add(enemyScript);
        enemyScript.OnQuit += EnemyQuit;
        enemyScript.speed += Random.Range(-speedRandomizer, speedRandomizer);
        enemyScript.pathCreator = paths[pathIndex];
    }

    private void EnemyQuit(Enemy enemy)
    {
        existingEnemies.Remove(enemy);
        // check whether the wave is over
        if (IsWaveOver())
        {
            // check whether there is no more wave
            if (currentWave == waveSpawners.Count - 1)
            {
                GameData.Instance.gameManager.GameEnd(true);
            }
            // go to store
            else
            {
                currentWave++;
                GameData.Instance.gameManager.StoreStart();
            }
        }
    }
    private bool IsWaveOver()
    {
        // first check whether all the spawners are disabled 
        // then check whether existingEnemies is empty
        
        var spawners = waveSpawners[currentWave];
        if (spawners.list.Any(spawner => spawner.enabled))
        {
            // still have some enemies to spawn
            return false;
        }
        return existingEnemies.Count == 0;
    }
    
    public void NewWaveStart()
    {
        var spawners = waveSpawners[currentWave];
        foreach (Spawner spawner in spawners.list)
        {
            spawner.gameObject.SetActive(true);
            // reset the index 
            spawner.Restart();
        }
    }



    


    
    
    

}
