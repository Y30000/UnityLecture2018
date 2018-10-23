using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleUIEventHandler : MonoBehaviour, IPointerClickHandler {

    public RectTransform root;

    public void OnPointerClick(PointerEventData eventData)
    {
        root.gameObject.SetActive(false);
        GameFlow.Instance.fsm.SetTrigger("StartGame");
    }
}
