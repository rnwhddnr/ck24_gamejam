using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class inven_item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image Item_image;

    public item item;
    [HideInInspector] public Transform Parent_After_Drag;

    public void Set_item(item new_item)
    {
        item = new_item;

        if (new_item.Item_Icon != null)
            Item_image = new_item.Item_Icon;
        else
            Item_image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Item_image.raycastTarget = false;
        Parent_After_Drag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Item_image.raycastTarget = true;
        transform.position = Parent_After_Drag.position;
        transform.SetParent(Parent_After_Drag);
    }
}
