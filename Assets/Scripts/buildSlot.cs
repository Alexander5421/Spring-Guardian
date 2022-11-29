using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildSlot : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int handIndex;
    public Tower currentTower;
    
    public void HaveTower(bool have)
    {
        spriteRenderer.enabled = !have;
    }
}
