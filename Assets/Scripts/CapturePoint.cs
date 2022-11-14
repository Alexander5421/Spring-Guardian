using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    //
    public event Action<int> caputured; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enemy captured");
        caputured?.Invoke(other.GetComponent<Enemy>().point);
        Destroy(other.gameObject);
    }
}
