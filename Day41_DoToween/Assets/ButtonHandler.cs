using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        print("OnPointerClick");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("OnPointerDown");
        transform.DOScale(0.8f, .1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("OnPointerUp");
        transform.DOScale(1f, .1f);
    }
}
