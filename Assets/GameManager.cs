using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager levelManager;
    public SpawnManager spawnManager;
    public StoreManager storeManager;
    public PlayerData playerData;
    public GameObject Store;
    public GameObject Level;
    public Tower[] towerPrefabs;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        {   
            Instance = this; 
        } 
    }

    public void SwitchToStoreMode()
    {
        Level.SetActive(false);
        Store.SetActive(true);
        playerData.playerHand.gameObject.SetActive(true);
    }

    private void Update()
    {
        //if I press the s key, I want to switch to the store mode
        if (Input.GetKeyDown(KeyCode.S))
        {
            SwitchToStoreMode();
        }
    }
}
