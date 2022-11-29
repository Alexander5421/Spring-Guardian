using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownMask : MonoBehaviour
{
    public float Progress
    {
        set
        {
            //check is the value is between 0 and 1
            transform.localScale = new Vector3(1, Mathf.Clamp01(value), 1);

        }
    }
    
}
