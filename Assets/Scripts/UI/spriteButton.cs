using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider2D))]
public class spriteButton : MonoBehaviour
{

    public UnityEvent OnClick = new UnityEvent();
    public UnityEvent OnRightClick = new UnityEvent();
    public void AddListener(UnityAction action)
    {
        OnClick.AddListener(action);
    }
    

    public void OnMouseOver () {
        if(Input.GetMouseButtonDown(0)){
            OnClick?.Invoke();
        }
        
        if(Input.GetMouseButtonDown(1)){
            //do stuff here
            OnRightClick?.Invoke();
        }
    }
}
