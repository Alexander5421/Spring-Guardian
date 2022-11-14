using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    // an Temporary implementation of the spawn manager
    
    // the spawn manager will be responsible for spawning the enemies and define the enemy spawn order and type
    
    [System.Serializable]
    public class Wave
    {
        // a delay before the spawn the first enemy
        public float delay;
        
        // list of enemies to spawn
        
        public List<Enemy> enemies;
    }
    
    public float speedRandomizer = 0.2f;
    
    public List<Wave> waves;

    public List<PathCreator> paths;

    public float timer;
    
    private IEnumerator WaveStart(int waveIndex)
    {
        if (waveIndex >= waves.Count)
        {
            // no more waves
            yield break;
        }
        // spawn the enemies in the wave
        var wave = waves[waveIndex];
        yield return new WaitForSeconds(wave.delay);
        int pathIndex = 0;
        foreach (var enemy in wave.enemies)
        {
            var enemyObject = Instantiate(enemy.gameObject);
            var enemyScript = enemyObject.GetComponent<Enemy>();
            enemyScript.speed += Random.Range(-speedRandomizer, speedRandomizer);
            // set the path for the enemy and in the order of the path
            enemyScript.pathCreator = paths[pathIndex++];
            pathIndex %= paths.Count;
        }
    }

    public void Start()
    {
        timer = 0;
        StartCoroutine("WaveStart", 0);
    }

    // call WaveStart every 10 seconds
    private void FixedUpdate()
    {
        // update the timer
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            StartCoroutine("WaveStart", 1);
            this.enabled = false;
        }
    }
}
