using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandVisible : MonoBehaviour, IPointerClickHandler
{
    public playerHand player_hand;
    public StoreManager store;
    public GameObject redArrow;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (player_hand.gameObject.activeSelf && !store.gameObject.activeSelf){
                player_hand.gameObject.SetActive(false);
            }

            if (redArrow.activeSelf)
            {
                redArrow.SetActive(false);
            }
        }
    }
}
