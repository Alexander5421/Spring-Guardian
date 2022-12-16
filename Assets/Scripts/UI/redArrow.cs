using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redArrow : MonoBehaviour
{

    private Vector3 _trans1;
    private Vector3 _trans2;

    private const float Hz = 1f;
    private const float Amplitude = 0.5f;

    private void OnEnable()
    {
        _trans1 = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _trans2 = _trans1;
        _trans2.y = Mathf.Sin(Time.fixedTime * Mathf.PI * Hz) * Amplitude + _trans1.y;

        transform.position = _trans2;
    }
}
