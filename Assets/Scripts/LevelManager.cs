using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public CapturePoint capturePoint;

    [SerializeField] private MMF_Player injuryFeedBacks;
    private void Start()
    {
        capturePoint.caputured += PlayerReceiveDmg;
    }

    private void PlayerReceiveDmg(int dmg)
    {
        injuryFeedBacks?.PlayFeedbacks();
        GameData.Instance.playerData.Health -= dmg;
        Debug.Log($"Player Health Update: {GameData.Instance.playerData.Health}");
        if (GameData.Instance.playerData.Health <= 0)
        {
            GameData.Instance.gameManager.GameEnd(false);
        }
    }
}
