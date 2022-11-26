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



    [ContextMenu("SwitchToLevelMode")]
    public void NewLevelStart ()
    {
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
        Level.SetActive(false);
        Store.SetActive(true);
        GameData.Instance.playerData.playerHand.gameObject.SetActive(true);
        mainCamera.backgroundColor = StoreColor;
    }
    
    public void GameEnd(bool win)
    {
        infoBoard.SetBoard(win? "You Win" : "Game Over");
        infoBoard.gameObject.SetActive(true);
        //pause game
        Time.timeScale = 0;
        
    }
    
    public void Restart()
    {
        //
        Debug.Log("Restart");
    }
    
}