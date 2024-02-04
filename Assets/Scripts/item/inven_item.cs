using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class inven_item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image Item_image;
    public TextMeshProUGUI Count_text;

    public item item;
    [HideInInspector] public Transform Parent_After_Drag;

    private Shop shop;
    private bool is_cook_item;
    private bool pointer;
    public bool is_result_slot;

    private int count = 0;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;

            if (count <= 0)
            {
                if (Inven_manager.instance.shop != null)
                    Inven_manager.instance.shop.delet_item(item);

                Destroy(gameObject);
            }
            else
            {
                Count_text.text = count.ToString();
            }
        }
    }

    private void Update()
    {
        if (Inven_manager.instance.cooking_mod)
        {
            if (shop == null)
                shop = FindObjectOfType<Shop>();

            if (Input.GetMouseButton(0) && pointer && Input.GetMouseButtonDown(1))
            {
                Set_food_slot(mouse_position_raycast_result());
            }
        }
    }
    private List<RaycastResult> mouse_position_raycast_result()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        return raycastResults;
    }

    public void Set_food_slot(List<RaycastResult> raycastResults)
    {
        Debug.Log(raycastResults);
        foreach (RaycastResult res in raycastResults)
        { 
            if (res.gameObject.GetComponent<inven_slot>() != null)
            {
                Inven_manager.instance.Spawn_new_item(item, res.gameObject.GetComponent<inven_slot>());
                res.gameObject.GetComponent<inven_slot>().Set_item(this);
                Count -= 1;
            }
        }
    }

    public void Set_item(item new_item)
    {
        item = new_item;
        Count += 1;

        if (new_item.Item_Icon != null)
            Item_image = new_item.Item_Icon;
        else
            Item_image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Item_image.raycastTarget = false;
        Parent_After_Drag = transform.parent;
        transform.SetParent(transform.parent.parent.parent.parent);

        if (is_result_slot)
        {
            Inven_manager.instance.shop.dest_nomal_slot();
            is_result_slot = false;
        }
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
        transform.SetAsFirstSibling();

        if (transform.parent.GetComponent<inven_slot>().is_cook_slot)
        {
            is_cook_item = true;
            Inven_manager.instance.shop.Set_item(item);
        }
        else if (!transform.parent.GetComponent<inven_slot>().is_cook_slot && is_cook_item)
        {
            Inven_manager.instance.shop.delet_item(item);
            is_cook_item = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointer = false;
    }
}
