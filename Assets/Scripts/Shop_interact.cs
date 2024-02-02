using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_interact : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    private GameObject choose_obj;
    private Shop shop;

    private void Start()
    {
        GetComponent<Interaction>().interact += active_choose;
        choose_obj = canvas.transform.Find("choose_panel").gameObject;
        shop = transform.GetComponentInParent<Shop>();
    }

    public void active_choose()
    {
        GameManager.instance.Can_interact = false;

        choose_obj.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "요리하기";
        choose_obj.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "요리하지않기";

        choose_obj.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        choose_obj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Button_start);
        choose_obj.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        choose_obj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(Button_not_start);

        choose_obj.SetActive(true);
    }

    public void Button_start()
    {
        choose_obj.SetActive(false);
        shop.Start_shop();
    }

    public void Button_not_start()
    {
        GameManager.instance.Can_interact = true;
        choose_obj.SetActive(false);
    }

    public void End_cook()
    {
        choose_obj.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "장사히기";
        choose_obj.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "장사하지않기";

        choose_obj.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        choose_obj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Button_start_sale);
        choose_obj.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        choose_obj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(Button_End_sale);
        
        choose_obj.SetActive(true);
        shop.End_shop();
    }

    public void Button_start_sale()
    {
        Inven_manager.instance.Coin += shop.sell_all_food();
        GameManager.instance.Can_interact = true;

        choose_obj.SetActive(false);
    }

    public void Button_End_sale()
    {
        GameManager.instance.Can_interact = true;
        choose_obj.SetActive(false);
    }
}
