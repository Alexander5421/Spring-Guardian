using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreCard : MonoBehaviour
{
    public  StoreManager storeManager;
    public  int towerType;
    public  Color freezeColor , normalColor;
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
    public SpriteRenderer spriteRenderer;
    public int level;
    public TextMeshPro priceText;
    // determine whether the card is sold out
    public GameObject card;
    public SpriteRenderer freezeButton;
    private int _price;

    public void freeze()
    {
        isFreeze = !isFreeze;
        freezeButton.color = isFreeze? freezeColor : normalColor;
        storeManager.Freeze(this);
    }

    public void Restore()
    {
        isFreeze = false;
    }

    public void Sell()
    {
        isFreeze = false;
        card.SetActive(false);
        freezeButton.color = isFreeze? freezeColor : normalColor;
    }

    public void OnCardSellButtonPressed()
    {
        storeManager.SellCard(this);
    }
}
