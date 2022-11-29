using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public SpriteRenderer cardRenderer;
    public GameObject foreGround;
    public spriteButton deleteButton;
    public int index;
    public CoolDownMask cooldownMask;
    public BoxCollider2D cooldownCollider2D;
    public float cooldownTime;
    public float cooldownTimer;
    public bool isOnCooldown;
    public void SetHandSlot(Sprite preview,int index)
    {
        cardRenderer.sprite = preview;
        foreGround.SetActive(true);
        this.index = index;
        
    }
    
    public void IntoCoolDown(float coolDownTime)
    {
        Debug.Log(coolDownTime);
        cooldownTimer = coolDownTime;
        this.cooldownTime = coolDownTime;
        cooldownCollider2D.gameObject.SetActive(true);
        cooldownMask.gameObject.SetActive(true); 
        isOnCooldown = true;
        
    }

    public void Update()
    {
        if (!isOnCooldown) return;
        cooldownTime -= Time.deltaTime;
        Debug.Log( cooldownTime);

        cooldownMask.Progress = cooldownTime/ cooldownTimer;
        if (cooldownTime <= 0)
        {
            cooldownTime = 0;
            isOnCooldown = false;
            cooldownCollider2D.gameObject.SetActive(false);
            cooldownMask.gameObject.SetActive(false); 
        }

    }
}
