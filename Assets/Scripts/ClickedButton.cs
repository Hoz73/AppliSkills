using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickedButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().color = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().color == Color.red ? Color.white : Color.red;
    }
}
