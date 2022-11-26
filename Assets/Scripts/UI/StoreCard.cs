using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreCard : MonoBehaviour
{
    public  int towerType;
    public  Sprite locked , unLocked;
    private bool isFreeze;
    public int price
    {
        get { return _price; }
        set
        {
            _price = value;
            priceText.text = _price.ToString();
        }
    }
    public SpriteRenderer cardPoster;
    public int level;
    public TextMeshPro priceText;
    public TextMeshPro nameText;
    // determine whether the card is sold out
    public GameObject card;
    public SpriteRenderer lockSprite;
    private int _price;

    public void Freeze()
    {
        isFreeze = !isFreeze;
        lockSprite.sprite = isFreeze? locked : unLocked;
        // change the transparency of the lock sprite
        lockSprite.color = isFreeze? new Color(1,1,1,1) : new Color(1,1,1,0.25f);
        
        GameData.Instance.storeManager.Freeze(this);
    }

    public void Restore()
    {
        isFreeze = false;
    }

    public void Sell()
    {
        isFreeze = false;
        card.SetActive(false);
        lockSprite.sprite = isFreeze? locked : unLocked;
        lockSprite.color = isFreeze? new Color(1,1,1,1) : new Color(1,1,1,0.5f);
    }

    public void OnCardSellButtonPressed()
    {
        GameData.Instance.storeManager.SellCard(this);
    }
}
