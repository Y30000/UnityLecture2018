using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RestartGameUIButtonHandler : MonoBehaviour, IPointerClickHandler , IPointerDownHandler, IPointerUpHandler
{
    public RectTransform root;

    public void OnPointerClick(PointerEventData eventData)
    {
        root.GetComponent<Image>().DOFade(0f, 0.5f).OnComplete(() =>    //완료된 이후
        {
            GameFlow.Instance.StartCoroutine(GameFlow.Instance.RestartGame());
            GameFlow.Instance.fsm.SetTrigger("RestartGame");
            root.gameObject.SetActive(false);
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
