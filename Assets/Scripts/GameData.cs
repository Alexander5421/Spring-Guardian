using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public LevelManager levelManager;
    public SpawnManager spawnManager;
    public StoreManager storeManager;
    public PlayerData playerData;
    public GameManager gameManager;
    public SoundManager soundManager;

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

}
