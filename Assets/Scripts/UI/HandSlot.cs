using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public SpriteRenderer cardRenderer;
    public GameObject foreGround;
    public spriteButton deleteButton;
    public int index;

    public void SetHandSlot(Sprite preview,int index)
    {
        cardRenderer.sprite = preview;
        foreGround.SetActive(true);
        this.index = index;
        
    }
    
    

}
