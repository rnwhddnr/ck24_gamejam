using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    [SerializeField] GameObject Shop_UI_group;
    [Space(10f)]

    public List<Recipe> recipes = new List<Recipe>();
    public List<item> items = new List<item>();
    public List<item> Food_items = new List<item>();

    public List<inven_slot> nomal_slot = new List<inven_slot>();
    public List<inven_slot> food_slot = new List<inven_slot>();
    public inven_slot Result_slot;
    private Transform inven;

    private void Start()
    {
        Inven_manager.instance.shop = this;
    }

    public void Start_shop()
    {
        GameManager.instance.Can_interact = false;
        Inven_manager.instance.cooking_mod = true;

        Shop_UI_group.SetActive(true);
        
        inven = Inven_manager.instance.inven.transform.Find("inven_group");
        inven.SetParent(Shop_UI_group.transform);
        
        RectTransform rect = inven.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        rect.localScale = new Vector2(1, 1);
        rect.sizeDelta = new Vector2(1920, 1080);
    }

    public void End_shop()
    {
        Inven_manager.instance.cooking_mod = false;

        Shop_UI_group.SetActive(false);

        inven.SetParent(Inven_manager.instance.inven.transform);
        RectTransform rect = inven.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        rect.localScale = new Vector2(1, 1);
        rect.sizeDelta = new Vector2(1920, 1080);
    }

    public void Set_item(item item)
    {
        items.Add(item);
        Butten_cook();
    }

    public void delet_item(item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }

    public void Butten_cook()
    {
        foreach (Recipe rec in recipes)
        {
            if (rec.Get_item(items.ToArray()) != null)
            {
                GameObject new_item_obj = Inven_manager.instance.Spawn_new_item(rec.Get_item(items.ToArray()), Result_slot);
                new_item_obj.tag = "Food";
                new_item_obj.GetComponent<inven_item>().is_result_slot = true;
                Food_items.Add(new_item_obj.GetComponent<inven_item>().item);

                return;
            }
        }
    }

    public bool sell_food(item item)
    {
        bool result = false;

        if (Food_items.Contains(item))
        {
            Inven_manager.instance.Coin += item.coin;
            Food_items.Remove(item);
            
            for (int i = 0;i < food_slot.Count;i++)
            {
                if (food_slot[i].GetComponentInChildren<inven_item>() != null)
                {
                    if (food_slot[i].GetComponentInChildren<inven_item>().item == item)
                    {
                        food_slot[i].GetComponentInChildren<inven_item>().Count -= 1;
                        result = true;
                    }
                }
            }
        }

        return result;
    }

    public void dest_nomal_slot()
    {
        for(int i = 0; i < nomal_slot.Count; i++)
        {
            if (nomal_slot[i].GetComponentInChildren<inven_item>() != null)
                nomal_slot[i].GetComponentInChildren<inven_item>().Count -= 1;
        }

        Butten_cook();
    }
}
