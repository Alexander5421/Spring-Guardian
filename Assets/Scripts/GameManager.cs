using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Store;
    public GameObject Level;
    public int waveNumber;
    public Camera mainCamera;
    public Color StoreColor, LevelColor;
    public InfoBoard infoBoard;
    public GameState gameState;


    public void Restart()
    {
        GameData.Instance.spawnManager.ResetWave();
        GameData.Instance.playerData.ResetPlayerData();
        Time.timeScale = 1;
        infoBoard.gameObject.SetActive(false);
        NewLevelStart();
    }

    [ContextMenu("SwitchToLevelMode")]
    public void NewLevelStart ()
    {
        gameState = GameState.InGame;
        // set health to max
        GameData.Instance.playerData.RestoreAllHealth();
        Level.SetActive(true);
        GameData.Instance.spawnManager.NewWaveStart();
        Store.SetActive(false);
        GameData.Instance.playerData.playerHand.gameObject.SetActive(false);
        mainCamera.backgroundColor = LevelColor;
    }

    [ContextMenu("SwitchToStoreMode")]
    public void StoreStart()
    {
        // find all gameobjects with tag "Enemy" and destroy them
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        gameState = GameState.Store;
        Level.SetActive(false);
        Store.SetActive(true);
        GameData.Instance.playerData.playerHand.gameObject.SetActive(true);
        mainCamera.backgroundColor = StoreColor;
    }

    public void GameEnd(bool win)
    {
        gameState = GameState.GameOver;
        Debug.Log("GameEnd");
        infoBoard.SetBoard(win? "You Win" : "Game Over");
        infoBoard.gameObject.SetActive(true);
        //pause game
        Time.timeScale = 0;
        
    }
}

public enum GameState
{
    Menu,
    Store,
    InGame,
    GameOver
}