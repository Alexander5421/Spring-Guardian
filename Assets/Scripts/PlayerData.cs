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
    private void RestoreAllHealth()
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
    public List<int> initialTowerList = new List<int>();
    public TextMeshPro moneyText;
    public TextMeshPro healthText;
    public playerHand playerHand;
    public CoolDown[] coolDownList = new CoolDown[10];
    public bool[] towerInHand = new bool[10];
    public buildSlot[] buildSlots;
    private buildSlot currentSlot;

    public void ResetPlayerData()
    {
        Money = 0;
        // TODO should link to the prefab 
        Health = 10;
        maxHealth = 10;
        towerList.Clear();
        // use the initial tower list to reset the tower list
        foreach (int tower in initialTowerList)
        {
            towerList.Add(tower);
        }
        GameData.Instance.playerData.RemoveAllTower();
        GameData.Instance.playerData.ResetCoolDown();
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



    #region Action_in_store
    /*
     * Add tower to player hand, called when player buy something in store 
     */
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
    #endregion


    public void NewWave()
    {
        RestoreAllHealth();
        // return all the card to hand
        for (int i = 0; i < towerInHand.Length; i++)
        {
            towerInHand[i] = true;
        }
        // player cannot delete tower in hand in wave
        playerHand.DisableAllButtons();
        // reset the cooldown of all towers
        foreach (CoolDown coolDown in coolDownList)
        {
            coolDown.isCoolingDown = false;
        }
        
    }

    #region Action_in_wave
    public StoreManager store;
    public void UseTower(HandSlot slot)
    {
        
        if (store.gameObject.activeSelf){
            return;
        }
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
        coolDownList[slot.handIndex].SetCoolDown(slot.currentTower.buildCooldown);
        Destroy(slot.currentTower.gameObject);
        slot.currentTower = null;
        slot.HaveTower(false);
        playerHand.RestoreCard(slot.handIndex);
    }

    private void RemoveTowerWithoutCooldown(buildSlot slot)
    {
        if (slot.currentTower==null)
        {
            return;
        }
        // set the tower into cooldown mode
        // return to player hand
        towerInHand[slot.handIndex] = true;
        Destroy(slot.currentTower.gameObject);
        slot.currentTower = null;
        slot.HaveTower(false);
        playerHand.RestoreCard(slot.handIndex);
    }

    public void RemoveAllTower()
    {
        foreach (buildSlot slot in buildSlots)
        {
            RemoveTowerWithoutCooldown(slot);   
        }
    }

    public GameObject redArrow;//the red arrow indicate player which slot is chosen
    
    // active player hand when player click a build slot
    public void OnBuildSlotClick( buildSlot slot){
        if (slot.currentTower!=null)
        {
            if (slot.currentTower.rangeIndicator.gameObject.activeSelf)
            {
                slot.currentTower.HideRange();
            }
            else
            {
                slot.currentTower.ShowRange();
            }
            return;
        }
        // no tower yet
        currentSlot = slot;
        playerHand.gameObject.SetActive(true);
        redArrow.SetActive(false);
        var add = new Vector3(0, 1.3f, 0);
        redArrow.transform.position = currentSlot.transform.position + add;
        redArrow.SetActive(true);
    }
    #endregion

    private void Awake()
    {
        //initialize the cooldown list
        for (int i = 0; i < coolDownList.Length; i++)
        {
            coolDownList[i] = new CoolDown();
        }
        
        // buildSlot get all component of buildSlot in the scene
        buildSlots = FindObjectsOfType<buildSlot>();
    }

    // active when player click the right mouse button
    private void Update()
    {
        // first check whether the player is building
        
        if (GameData.Instance.gameManager.gameState==GameState.InGame && currentSlot != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                DisableBuild();
            }
        }
        
        // cool down
        foreach (CoolDown coolDown in coolDownList)
        {
            coolDown.Update(Time.deltaTime);
        }
        foreach (HandSlot card in playerHand.cards)
        {
            if (card.index >= towerList.Count)
            {
                continue;
            }
            CoolDown coolDown = coolDownList[card.index];
            card.Refresh(coolDown.coolDownTime,coolDown.coolDownTimer,coolDown.isCoolingDown);
        }
    }
    
    // reset all the cooldown
    public void ResetCoolDown()
    {
        foreach (CoolDown coolDown in coolDownList)
        {
            coolDown.isCoolingDown = false;
        }
    }

    public void CancelBuild()
    {
        currentSlot = null;
        playerHand.gameObject.SetActive(false);
    }
    
    public void DisableBuild(){
        redArrow.SetActive(false);
        playerHand.gameObject.SetActive(false);
    }
}
