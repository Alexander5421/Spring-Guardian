using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.text = GameData.Instance.gameManager.gameState.ToString();
    }
}
