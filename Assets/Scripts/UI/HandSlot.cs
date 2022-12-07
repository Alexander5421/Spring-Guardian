using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public SpriteRenderer cardRenderer;
    public GameObject foreGround;
    public SlotButton deleteButton;
    public int index;
    public CoolDownMask cooldownMask;
    public BoxCollider2D cooldownCollider2D;
    public void SetHandSlot(Sprite preview,int index)
    {
        cardRenderer.sprite = preview;
        foreGround.SetActive(true);
        this.index = index;
        
    }


    public void Refresh(float cooldownTime,float cooldownTimer,bool isOnCoolDown)
    {
        cooldownCollider2D.gameObject.SetActive(isOnCoolDown);
        cooldownMask.gameObject.SetActive(isOnCoolDown); 
        if (isOnCoolDown)
        {
            cooldownMask.Progress = cooldownTime/ cooldownTimer;
        }
    }
}
