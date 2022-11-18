using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class spriteButton : MonoBehaviour
{

    public UnityEvent OnClick = new UnityEvent();

    public void AddListener(UnityAction action)
    {
        OnClick.AddListener(action);
    }
    

    public void OnMouseDown()
    {
        OnClick.Invoke();
    }
}
