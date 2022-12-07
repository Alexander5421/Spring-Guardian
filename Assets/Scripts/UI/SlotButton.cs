using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SlotButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClick = new UnityEvent();
    public UnityEvent OnRightClick = new UnityEvent();

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Use this to tell when the user right-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick?.Invoke();
        }

        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            OnClick?.Invoke();
        }
    }
}
