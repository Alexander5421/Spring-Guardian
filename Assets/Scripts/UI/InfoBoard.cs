using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoBoard : MonoBehaviour
{
    public TextMeshPro info;
    

    public void SetBoard(string text)
    {
        info.text = text;
    }
    
    

}