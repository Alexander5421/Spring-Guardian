using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHand : MonoBehaviour
{
    // manage the player hand's card ui
    public HandSlot cardPrefab;
    public PlayerData playerData;
    public List<HandSlot> cards = new List<HandSlot>();
    public Sprite[] cardSprites;
    [ContextMenu("createSlots")]
    private void CreateSlots()
    {
        // add ten cards to the transform from left to right and no gap between them the size of the card is 3.125
        for (int i = 0; i < 10; i++)
        {
            HandSlot card = Instantiate<HandSlot>(cardPrefab, transform);
            card.gameObject.SetActive(true);
            card.transform.localPosition = new Vector3(i * 3.125f, 0, 0);
            cards.Add(card);
        }
    }

    private void Start()
    {
        foreach (HandSlot card in cards)
        {
            card.foreGround.SetActive(false);
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (i < playerData.towerList.Count)
            {
                cards[i].SetHandSlot(cardSprites[playerData.towerList[i]], i);
            }
            else
            {
                cards[i].foreGround.SetActive(false);
            }
        }
    }
}
