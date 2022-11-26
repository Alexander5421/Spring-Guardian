using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBar;
    public float healthRatio
    {
        get { return healthBar.transform.localScale.x; }
        set { healthBar.transform.localScale = new Vector3(value, 1, 1); }
    }
}
