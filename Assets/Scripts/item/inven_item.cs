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
    [HideInInspector] public bool is_cook_item;
    private bool pointer;

    private int count = 0;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;

            if (count <= 0)
                Destroy(gameObject);
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

            if (Input.GetMouseButtonDown(1) && pointer)
            {
                if (!is_cook_item)
                {
                    shop.Set_nomal_slot(item);
                    Count -= 1;
                }
                else
                {
                    Inven_manager.instance.Add_new_item(item);
                    Destroy(gameObject);
                }
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
        transform.SetParent(transform.parent.parent.parent);
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
