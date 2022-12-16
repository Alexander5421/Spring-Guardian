using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] bgms;

    public void Play(int index)
    {
        // pause others 
        foreach (AudioSource source in bgms)
        {
            // if active stop
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
        bgms[index].Play();
        bgms[index].loop = true;
    }
    
}
