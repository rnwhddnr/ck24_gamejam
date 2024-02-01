using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inven_manager : MonoBehaviour
{
    public static Inven_manager instance;

    public GameObject inven;
    
    public List<item> Inven_items = new List<item>();
    public List<inven_slot> Inven_slot = new List<inven_slot>();
    [Space(10f)]

    public GameObject Item_prefeb;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        inven_control();
    }

    private void inven_control()
    {
        if (SceneManager.GetActiveScene().name == "Main")
            return;

        if (Input.GetKeyDown(GameManager.instance.OperationKey["Inventory"]))
        {
            inven.SetActive(!inven.activeSelf);
        }
    }

    public void Add_new_item(item item)
    {
        Inven_items.Add(item);

        for (int i = 0; i < Inven_slot.Count; i++)
        {
            inven_slot slot = Inven_slot[i];
            inven_item item_in_slot = slot.GetComponentInChildren<inven_item>();

            if (item_in_slot == null)
            {
                Spawn_new_item(item, slot);
                return;
            }
        }
    }

    private void Spawn_new_item(item item, inven_slot slot)
    {
        GameObject new_item_obj = Instantiate(Item_prefeb, slot.transform);
        inven_item new_inven_item = new_item_obj.GetComponent<inven_item>();

        new_inven_item.Set_item(item);
    }
}
