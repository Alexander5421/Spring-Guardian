using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public SpriteRenderer renderer;
    public GameObject foreGround;
    public spriteButton deleteButton;
    public int index;

    public void SetHandSlot(Sprite preview,int index)
    {
        renderer.sprite = preview;
        foreGround.SetActive(true);
        this.index = index;
        
    }
    
    

}
