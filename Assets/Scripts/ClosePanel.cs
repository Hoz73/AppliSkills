using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
