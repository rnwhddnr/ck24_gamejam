using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Inven_manager : MonoBehaviour
{
    public static Inven_manager instance;

    public GameObject inven;

    public List<item> Inven_items = new List<item>();
    public List<inven_slot> Inven_slot = new List<inven_slot>();
    [Space(10f)]

    public GameObject Item_prefeb;
    public Shop shop;

    [SerializeField] private int coin;
    public int Coin
    {
        get { return coin; }
        set
        {
            if (coin != value)
                coin = value;

            coin_text.text = coin.ToString();
        }
    }
    [SerializeField] TextMeshProUGUI coin_text;

    [SerializeField] item[] test_add_items = new item[0];
    [HideInInspector] public bool cooking_mod;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        foreach (item i in test_add_items)
            Add_new_item(i);
    }

    private void Update()
    {
        inven_control();
    }

    private void inven_control()
    {
        if (!GameManager.instance.Can_interact)
            return;

        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "Load")
            return;

        if (Input.GetKeyDown(GameManager.instance.OperationKey["Inventory"]))
        {
            inven.SetActive(!inven.activeSelf);
        }
    }

    public void Add_new_item(item item)
    {
        Inven_items.Add(item);
        
        inven_slot first_null_slot = null;
        for (int i = 0; i < Inven_slot.Count; i++)
        {
            inven_slot slot = Inven_slot[i];
            
            if (slot.GetComponentInChildren<inven_item>() == null)
            {
                if (first_null_slot == null)
                    first_null_slot = slot;
                continue;
            }

            inven_item item_in_slot = slot.GetComponentInChildren<inven_item>();

            if (item_in_slot.item.Item_Name == item.Item_Name)
            {
                Spawn_new_item(item, slot);
                return;
            }
        }
        Spawn_new_item(item, first_null_slot);
        return;
    }

    public GameObject Spawn_new_item(item item, inven_slot slot)
    {
        GameObject new_item_obj = null;

        if (slot.GetComponentInChildren<inven_item>() != null)
            slot.GetComponentInChildren<inven_item>().Count += 1;
        else
        {
            new_item_obj = Instantiate(Item_prefeb, slot.transform);
            new_item_obj.transform.SetAsFirstSibling();
            inven_item new_inven_item = new_item_obj.GetComponent<inven_item>();

            new_inven_item.Set_item(item);
        }

        return new_item_obj;
    }
}
