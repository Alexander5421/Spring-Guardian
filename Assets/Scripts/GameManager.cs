using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject Store;
    public GameObject Level;
    public int waveNumber;
    public Camera mainCamera;
    public Color StoreColor, LevelColor;
    public InfoBoard infoBoard;
    public GameState gameState;

    //TODO call after player hit start in the main menu
    private void Start()
    {
        ReturnToMenu();
    }

    private void StartMenu()
    {
        gameState = GameState.Menu;
        startMenu.SetActive(true);
    }

    private void Update()
    {
        if (gameState == GameState.Menu)
        {
            if (Input.anyKeyDown)
            {
                startMenu.SetActive(false);
                NewLevelStart();
                // if the game is in the menu state
            }
        }
        // if any key is pressed
        
    }
    
    public void ReturnToMenu()
    {
        RestartWithoutIntoNewLevel();
        StartMenu();
        Store.SetActive(false);
        Level.SetActive(false);
    }

    public void Restart()
    {
        RestartWithoutIntoNewLevel();
        NewLevelStart();
    }
    public void RestartWithoutIntoNewLevel()
    {
        GameData.Instance.spawnManager.ResetWave();
        GameData.Instance.playerData.ResetPlayerData();
        Time.timeScale = 1;
        infoBoard.gameObject.SetActive(false);
    }

    [ContextMenu("SwitchToLevelMode")]
    public void NewLevelStart ()
    {
        gameState = GameState.InGame;
        // set health to max
        GameData.Instance.playerData.NewWave();
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
        // some enemies still in death animation,not destoried
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GameData.Instance.playerData.RemoveAllTower();
        GameData.Instance.playerData.ResetCoolDown();
        GameData.Instance.playerData.playerHand.EnableAllButtons();
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