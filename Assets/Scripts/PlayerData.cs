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
}
