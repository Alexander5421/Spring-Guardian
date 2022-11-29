using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{   
    
    /// <summary>
    /// constant variables the upper limit of towerList
    /// </summary>
    int maxTower = 10;
    [SerializeField]
    int money = 0;
    int health = 10;
    int maxHealth = 10;
    public  int Money 
    {
        get { return money; }
        set
        {
            money = value;
            // change the text of the money text
            moneyText.text = money.ToString();
        }
    }
    public void RestoreAllHealth()
    {
        Health = maxHealth;
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            // if health is less than 0, text should be 0
            if (health < 0)
            {
                healthText.text = 0.ToString();
            }
            else
            {
                
                healthText.text = health.ToString();
            }
            
        }
    }
    
    // the tower that is in player hand
    public List<int> towerList = new List<int>();
    public TextMeshPro moneyText;
    public TextMeshPro healthText;
    public playerHand playerHand;
    public bool[] towerInHand = new bool[10];
    public buildSlot[] buildSlots;
    private buildSlot currentSlot;

    public void ResetPlayerData()
    {
        Money = 0;
        Health = 10;
        maxHealth = 10;
        towerList.Clear();
        playerHand.Refresh();
        for (int i = 0; i < towerInHand.Length; i++)
        {
            towerInHand[i] = false;
        }

    }
    
    [ContextMenu("AddMoney")]
    private void AddMoney()
    {
        Money += 100;
        
    }
    
    public void MoneyChange(int value)
    {
        Money += value;
    }
    
    public void AddTower(int towerID)
    {
        towerList.Add(towerID);
        // check whether there is three same tower in the list the id is towerId
        if ( towerID%2==0 && towerList.FindAll(x => x == towerID).Count >= 3)
        {
            // if there is three same tower in the list, remove them
            towerList.RemoveAll(x => x == towerID);
            // add the tower to player hand
            towerList.Add(towerID+1);
        }
        playerHand.Refresh();
    }
    
    public bool CanAddTower()
    {
        return towerList.Count < maxTower;
    }
    
    public void DeleteTower(HandSlot slot)
    {
         towerList.RemoveAt(slot.index);
         playerHand.Refresh();
    }

    public void Start()
    {
        NewWave();
    }

    public void NewWave()
    {
        // set the tower in hand to true
        for (int i = 0; i < towerInHand.Length; i++)
        {
            towerInHand[i] = true;
        }
        // player cannot delete tower in hand
        playerHand.DisableAllButtons();
    }

    public void UseTower(HandSlot slot)
    {
        int index = slot.index;
        // test whether index is in the range of towerList
        if (index >= towerList.Count)
        {
            return;
        }
        // test whether the tower is in hand
        if (!towerInHand[index])
        {
            return;
        }
        // set the tower in hand to false
        towerInHand[index] = false;
        currentSlot.handIndex = index;
        playerHand.FadeCard(index);
        DisableBuild();
        
        // instantiate the tower
        Tower tower =Instantiate(GameData.Instance.towerPrefabs[towerList[index]], currentSlot.transform.position, Quaternion.identity);
        // make the tower local postion z 0.1
        tower.transform.localPosition += Vector3.forward*0.1f; 
        currentSlot.currentTower = tower;
        tower.transform.parent = currentSlot.transform;
        currentSlot.HaveTower(true);
    }

    public void RemoveTower(buildSlot slot)
    {
        if (slot.currentTower==null)
        {
            return;
        }
        // set the tower into cooldown mode
        // return to player hand
        towerInHand[slot.handIndex] = true;
        playerHand.cards[slot.handIndex].IntoCoolDown(slot.currentTower.buildCooldown);
        Destroy(slot.currentTower.gameObject);
        slot.currentTower = null;
        slot.HaveTower(false);
        playerHand.RestoreCard(slot.handIndex);
    }
    
    // 
    public void EnableBuild( buildSlot slot){
        if (slot.currentTower!=null)
        {
            return;
        }
        currentSlot = slot;
        playerHand.gameObject.SetActive(true);
    }
    // active when player click the right mouse button

    private void Update()
    {
        // first check whether the player is building
        if (currentSlot != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                DisableBuild();
            }
        }
    }

    public void CancelBuild()
    {
        currentSlot = null;
        playerHand.gameObject.SetActive(false);
    }
    
    public void DisableBuild(){
        playerHand.gameObject.SetActive(false);
    }
}
