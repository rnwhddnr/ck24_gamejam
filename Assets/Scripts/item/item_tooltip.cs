using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class item_tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tooltip;
    private item item;
    private RectTransform rect;

    private void Start()
    {
        item = GetComponent<inven_item>().item;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip = GameObject.Find("inven_group").transform.Find("tooltip").gameObject;

        if (tooltip == null)
            return;

        tooltip.transform.Find("item_image").GetComponent<Image>().sprite = item.Item_Icon;
        tooltip.transform.Find("item_name").GetComponent<TextMeshProUGUI>().text = item.Item_Name;
        tooltip.transform.Find("item_de").GetComponent<TextMeshProUGUI>().text = item.Item_Description;

        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip == null)
            return;

        tooltip.SetActive(false);
    }

    private void Update()
    {
        if (tooltip == null)
            return;

        if (tooltip.activeSelf == true)
        {
            if (rect == null)
                rect = tooltip.GetComponent<RectTransform>();

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