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
        GameManager.Instance.storeManager.Freeze(this);
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
    }

    public void OnCardSellButtonPressed()
    {
        GameManager.Instance.storeManager.SellCard(this);
    }
}
