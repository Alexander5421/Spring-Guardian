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
    // the tower that is in player hand
    public List<int> towerList = new List<int>();
    public TextMeshPro moneyText;
    public playerHand playerHand;
    public bool[] towerInHand = new bool[10];
    public buildSlot[] buildSlots;
    private buildSlot currentSlot;
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
        playerHand.FadeCard(index);
        DisableBuild();
        
        // instantiate the tower
        var tower =Instantiate(GameManager.Instance.towerPrefabs[towerList[index]], currentSlot.transform.position, Quaternion.identity);
        tower.transform.parent = currentSlot.transform;
        currentSlot.spriteRenderer.sprite = null;
    }
    
    // 
    public void EnableBuild( buildSlot slot){
        currentSlot = slot;
        playerHand.gameObject.SetActive(true);
    }
    
    public void DisableBuild(){
        playerHand.gameObject.SetActive(false);
    }
}
