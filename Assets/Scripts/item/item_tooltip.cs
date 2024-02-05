using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class item_tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject tooltip;
    private item item;
    private RectTransform rect;

    private void Start()
    {
        item = GetComponent<item>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.transform.Find("item_image").GetComponent<Image>().sprite = item.Item_Icon;
        tooltip.transform.Find("item_name").GetComponent<TextMeshProUGUI>().text = item.Item_Name;
        tooltip.transform.Find("item_de").GetComponent<TextMeshProUGUI>().text = item.Item_Description;

        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.activeSelf == true)
        {
            tooltip.transform.position = Input.mousePosition;

            if (rect.anchoredPosition.x + rect.sizeDelta.x > Screen.width && rect.anchoredPosition.y + rect.sizeDelta.y > Screen.height)
                rect.pivot = new Vector2(1, 1);
            else if (rect.anchoredPosition.x + rect.sizeDelta.x > Screen.width)
                rect.pivot = new Vector2(1, 0);
            else if (rect.anchoredPosition.y + rect.sizeDelta.y > Screen.height)
                rect.pivot = new Vector2(0, 1);
            else
                rect.pivot = new Vector2(0, 0);
        }
    }

}