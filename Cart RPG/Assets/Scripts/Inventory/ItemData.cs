using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Item item;
    public int amount = 1;

    public void OnBeginDrag(PointerEventData eventData) {
        if (item != null) {
            this.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (item != null) {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
    }
}
