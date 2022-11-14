using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int playerHealth = 10;
    public CapturePoint capturePoint;
    private void Start()
    {
        capturePoint.caputured += PlayerReceiveDmg;
    }

    private void PlayerReceiveDmg(int dmg)
    {
        playerHealth -= dmg;
        Debug.Log($"Player Health Update: {playerHealth}");
        if (playerHealth <= 0)
        {
            Debug.Log("Game Over");
            
        }
    }
}
