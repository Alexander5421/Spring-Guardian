using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaker : MonoBehaviour
{
    public event Action OnEvent;
    
    //context menue 
    [ContextMenu("Event Happen")]
    public void Die()
    {
        OnEvent?.Invoke();
    }
}
