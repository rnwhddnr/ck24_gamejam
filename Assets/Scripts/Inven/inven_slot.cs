using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class inven_slot : MonoBehaviour, IDropHandler
{
    [SerializeField] string Tag_name;
    public bool is_cook_slot;

    public void OnDrop(PointerEventData eventData)
    {
        inven_item Drop_item = eventData.pointerDrag.GetComponent<inven_item>();

        if (Tag_name == "")
            Drop_item.Parent_After_Drag = transform;
        else
            if (Drop_item.CompareTag(Tag_name))
                Drop_item.Parent_After_Drag = transform;
    }

    public void Set_item(inven_item item)
    {
        if (is_cook_slot)
            FindObjectOfType<Shop>().Butten_cook();
    }
}
