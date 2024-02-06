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
    public GameObject prefeb_cust;
    private int ran_count;
    private int cnt = 0;

    [SerializeField] Camera cut_cam;
    [SerializeField] Camera main_cam;
    [SerializeField] Transform pos;
    private bool bb;
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
        choose_obj.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "장사하기";
        choose_obj.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "장사하지않기";

        choose_obj.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        choose_obj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Button_start_sale);
        choose_obj.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        choose_obj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(Button_End_sale);
        
        choose_obj.SetActive(true);
        shop.End_shop();
    }

    private void Update()
    {
        if (bb)
            FindObjectOfType<Player>().transform.position = new Vector2(-22, -2);

    }

    public void Button_start_sale()
    {
        GameManager.instance.Can_move = false;
        Player player = FindObjectOfType<Player>();
        player.canvas.gameObject.SetActive(false);
        player.transform.position = new Vector2(-22, -2);
        player.cant_cam = true;

        bb = true;
        cut_cam.enabled = true;
        main_cam.enabled = false;

        cut_cam.transform.position = main_cam.transform.position; 

        Inven_manager.instance.shop.gameObject.layer = 0;
        ran_count = Random.Range(4, 7);
        cut_cam.orthographicSize = 5;
        spwan_customer();

        choose_obj.SetActive(false);

        player.transform.position = transform.parent.GetChild(0).position;
        player.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Button_End_sale()
    {
        GameManager.instance.Can_interact = true;
        GameManager.instance.Can_move = true;
        FindObjectOfType<Player>().canvas.gameObject.SetActive(true);
        cut_cam.enabled = false;
        main_cam.enabled = true;
        main_cam.transform.position = cut_cam.transform.position;

        Inven_manager.instance.shop.gameObject.layer = 9;
        choose_obj.SetActive(false);
        bb = false;

        GameObject player = GameObject.Find("Player");
        player.transform.GetChild(1).gameObject.SetActive(true);

        player.GetComponent<Player>().cant_cam = false;
    }

    public void spwan_customer()
    {
        if (ran_count <= cnt)
        {
            Button_End_sale();
            return;
        }
        else
            cnt++;
        
        Instantiate(prefeb_cust);
    }
}
