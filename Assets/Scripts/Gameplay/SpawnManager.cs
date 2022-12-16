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

    private int spawnIndex = 0;
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
        enemyObject.name = enemyPrefab.name+"_"+spawnIndex++; 
        Enemy enemyScript = enemyObject.GetComponent<Enemy>();
        existingEnemies.Add(enemyScript);
        enemyScript.OnQuit += EnemyQuit;
        enemyScript.speed += Random.Range(-speedRandomizer, speedRandomizer);
        enemyScript.pathCreator = paths[pathIndex];
    }

    public void RemoveAllspawner(){
        var spawners = waveSpawners[currentWave];
        foreach (Spawner spawner in spawners.list)
        {
            spawner.gameObject.SetActive(false);
        }
    }

    private void EnemyQuit(Enemy enemy)
    {
        existingEnemies.Remove(enemy);
        // print ("Caller: "+enemy.name + $"enemy Left Number{ existingEnemies.Count}");
        // check whether the wave is over
        if (IsWaveOver())
        {
            RemoveAllspawner();
            // print(currentWave);
            // check whether there is no more wave
            if (currentWave == waveSpawners.Count - 1)
            {
                if (GameData.Instance.playerData.Health > 0){
                    GameData.Instance.gameManager.GameEnd(true);
                }
            }
            // go to store
            else
            {
                // if (GameData.Instance.gameManager.gameState == GameState.Store)
                // {
                //     return;
                // }
                if (GameData.Instance.playerData.Health > 0){
                    currentWave++;
                    GameData.Instance.gameManager.StoreStart();
                }
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
        //check whether there is no more wave
        if (currentWave == waveSpawners.Count - 1)
        {
            // play different music
            GameData.Instance.soundManager.Play(3);
        }
        var spawners = waveSpawners[currentWave];
        foreach (Spawner spawner in spawners.list)
        {
            spawner.gameObject.SetActive(true);
            // reset the index 
            spawner.Restart();
        }
    }

    public void ResetWave()
    {
        currentWave = 0;
        foreach (Enemy enemy in existingEnemies)
        {
            Destroy(enemy.gameObject);
        }
        existingEnemies.Clear();
    }
    

    


    
    
    

}
