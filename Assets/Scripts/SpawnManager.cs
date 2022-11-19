using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public float speedRandomizer = 0.2f;

    public List<PathCreator> paths;
    
    public List<Enemy> enemyPrefabs;
    
    public List<Enemy> existingEnemies = new List<Enemy>();


    // TODO determine the enemy orientation based on the path
    public void SpawnEnemy(int enemyIndex, int pathIndex)
    {
        Enemy enemyPrefab = enemyPrefabs[enemyIndex];
        GameObject enemyObject = Instantiate(enemyPrefab.gameObject);
        enemyObject.transform.parent = transform;
        Enemy enemyScript = enemyObject.GetComponent<Enemy>();
        existingEnemies.Add(enemyScript);
        enemyScript.OnQuit += EnemyQuit;
        enemyScript.speed += Random.Range(-speedRandomizer, speedRandomizer);
        enemyScript.pathCreator = paths[pathIndex];
    }
    
    public void EnemyQuit(Enemy enemy)
    {
        existingEnemies.Remove(enemy);
    }
    
    

    


    
    
    

}
