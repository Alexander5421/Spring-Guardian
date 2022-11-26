using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [Serializable]
    [Tooltip("The list of all the possible sellable items in the store")]
    public class CardPool
    {
        public int[] cardPool;
    }
    public SpriteRenderer storeLevelIndicator;
    public Sprite[] storeLevelSprites;
    public int level;
    public int[] levelCost;
    public Sprite[] carSprites;
    public string[] cardNames;
    public CardPool[] cardPool;
    public StoreCard storeCardPrefab;
    public StoreCard[] currentSellCard = new StoreCard[5];
    public bool[] isCardFreeze = new bool[5];
    public spriteButton refreshButton;
    public PlayerData playerData;
    public float width;
    public float padding;


    [ContextMenu("Create Store Slots")]
    private void InitializeCards()
    {
        // instantiate five storeCard objects based on prefab, with padding
        // use the prefab's position as the starting point for the first card
        // for the following card positions, use the previous card's position plus the width of the card plus padding
        
        
        Vector3 cardPosition = storeCardPrefab.transform.position;
        for (int i = 0; i < currentSellCard.Length; i++)
        {
            // handle scale and position
            currentSellCard[i] = Instantiate(storeCardPrefab, transform);
            currentSellCard[i].gameObject.SetActive(true);
            currentSellCard[i].transform.position = cardPosition;
            currentSellCard[i].name = "Store Card " + i;
            cardPosition.x += width + padding;
        }
        // disable the prefab
        storeCardPrefab.gameObject.SetActive(false);
        

    }

    private void Awake()
    {
        InitializeCards();
    }

    private void OnEnable()
    {
        Refresh();
    }
    
    

    public void Refresh()
    {
        // randomly pick 5 card from cardPool and put them into currentSellCard;
        // if the card is freeze, skip it and pick another one
        
        for (int i=0;i<5;i++)
        {
            if (isCardFreeze[i])
            {
                continue;
            }
            int randomIndex = UnityEngine.Random.Range(0, cardPool[level].cardPool.Length);
            currentSellCard[i].towerType = cardPool[level].cardPool[randomIndex];
            currentSellCard[i].cardPoster.sprite = carSprites[cardPool[level].cardPool[randomIndex]];
            currentSellCard[i].price = level * 10+5;
            currentSellCard[i].nameText.text = cardNames[cardPool[level].cardPool[randomIndex]];
            currentSellCard[i].card.SetActive(true);
            currentSellCard[i].Restore();
        }
        
    }

    public void Freeze(StoreCard storeCard)
    {
        // freeze the card
        // if the card is already freeze, unfreeze it
        // find the index of the card in currentSellCard
        // set the isCardFreeze[index] to true or false
       
        int index = Array.IndexOf(currentSellCard, storeCard);
        isCardFreeze[index] = !isCardFreeze[index];
       
        
    }
    
    public void UpgradeLevel()
    {
        if (level<cardPool.Length-1)
        {
            level++;
            // update the storeLevelIndicator
            // the sprite file is star_old_blight_+level
            storeLevelIndicator.sprite = storeLevelSprites[level];
        }
    }

    public void SellCard(StoreCard storeCard)
    {
        // test if the player has enough money
        // if yes, add the card to playerData
        // if no, do nothing
        if (playerData.Money >= storeCard.price && playerData.CanAddTower())
        {
            playerData.Money -= storeCard.price;
            playerData.AddTower(storeCard.towerType);
            storeCard.Sell();
            isCardFreeze[Array.IndexOf(currentSellCard, storeCard)] = false;
        }
    }
    
    public void LeaveStore()
    {
        GameData.Instance.gameManager.NewLevelStart();
    }
    
}
