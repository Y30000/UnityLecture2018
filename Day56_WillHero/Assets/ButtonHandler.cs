using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public RectTransform root;

    public void OnPointerClick(PointerEventData eventData)
    {
        root.GetComponent<Image>().DOFade(0f, 0.5f).OnComplete(() =>    //완료된 이후
        {
            root.gameObject.SetActive(false);
            GameFlow.Instance.fsm.SetTrigger("CloseUI");
        });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(0.8f, 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.1f);
    }
}
