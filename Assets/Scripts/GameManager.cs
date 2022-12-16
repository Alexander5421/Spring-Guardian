using System;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject Store;
    public StoreManager currentStore;
    public GameObject Level;
    public int waveNumber;
    public Camera mainCamera;
    public Color StoreColor, LevelColor;
    public InfoBoard infoBoard;
    public GameState gameState;
    public GameObject UI_heart;
    public GameObject UI_money;
    public GameObject blanket;
    public SpawnManager currentSpawns;
    public MMF_Player transFeedbacks;
    private MMF_Events eventFeedBacks;
    // context menu for testing triple the game speed
    private void Awake()
    {
        transFeedbacks.Initialization();
        eventFeedBacks = transFeedbacks.GetFeedbackOfType<MMF_Events>();
    }

    private void Trans(UnityAction callback)
    {
        eventFeedBacks.PlayEvents.AddListener(() =>
        {
            callback.Invoke();
            // unregister the event
            eventFeedBacks.PlayEvents.RemoveListener(callback);
        });
        transFeedbacks.PlayFeedbacks();
    }

    [ContextMenu("Triple Game Speed")]
    public void TripleGameSpeed()
    {
        Time.timeScale = 3;
    }
    // return to normal game speed
    [ContextMenu("Normal Game Speed")]
    public void NormalGameSpeed()
    {
        Time.timeScale = 1;
    }
    //TODO call after player hit start in the main menu
    private void Start()
    {
        RestartWithoutIntoNewLevel();
        StartMenu();
        Store.SetActive(false);
        Level.SetActive(false);
        UI_heart.SetActive(false);
        UI_money.SetActive(false);
        blanket.SetActive(false);
    }

    private void StartMenu()
    {
        gameState = GameState.Menu;
        startMenu.SetActive(true);
        GameData.Instance.soundManager.Play(0);
    }

    private void Update()
    {
        if (gameState == GameState.Menu)
        {
            if (Input.anyKeyDown)
            {
                Trans((() =>
                {
                    startMenu.SetActive(false);
                    UI_heart.SetActive(true);
                    UI_money.SetActive(true);
                    NewLevelStart();
                }));
                // if the game is in the menu state
            }
        }
        // if any key is pressed
        
    }
    
    public void ReturnToMenu()
    {
        Trans(Start);
    }

    public void Restart()
    {
        Trans((() =>
        {
            RestartWithoutIntoNewLevel();
            NewLevelStart();
        }));
        
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
        GameData.Instance.soundManager.Play(2);
        blanket.SetActive(true);
        gameState = GameState.InGame;
        // set health to max
        GameData.Instance.playerData.NewWave();
        Level.SetActive(true);
        GameData.Instance.spawnManager.NewWaveStart();
        Store.SetActive(false);
        GameData.Instance.playerData.playerHand.gameObject.SetActive(false);
        mainCamera.backgroundColor = LevelColor;
    }

    public void NewLevelStartWithTrans()
    {
        Trans((NewLevelStart));
    }
    

    [ContextMenu("SwitchToStoreMode")]
    public void StoreStart()
    {
        Trans((() =>
        {
            GameData.Instance.soundManager.Play(1);
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
            blanket.SetActive(false);
            Store.SetActive(true);
            GameData.Instance.playerData.playerHand.gameObject.SetActive(true);
            mainCamera.backgroundColor = StoreColor;
        }));
        
    }

    public void GameEnd(bool win)
    {
        Time.timeScale = 0;
        gameState = GameState.GameOver;
        Debug.Log("GameEnd");
        blanket.SetActive(false);
        infoBoard.SetBoard(win? "You Win" : "Game Over");
        infoBoard.gameObject.SetActive(true);
        currentSpawns.RemoveAllspawner();
        try{
            currentStore.Restart();
        }
        catch(NullReferenceException){
            // happen when player die in the first wave. 
            Debug.Log("we have not already initialize the shop, skip...");
        }
    }
}

public enum GameState
{
    Menu,
    Store,
    InGame,
    GameOver
}