using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "GameData/WaveData")]
public class WaveData : ScriptableObject
{
   // enemy type you want to spawn
   public int[] enemyTypes;
   public float[] timeInterval;
}
