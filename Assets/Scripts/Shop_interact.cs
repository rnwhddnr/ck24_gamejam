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
        Vector3 pos = Vector3.Lerp(Camera.main.transform.position, new Vector3(-4, -1, -10), 0.05f);
        Camera.main.transform.position = new Vector3(pos.x, pos.y, -10f);

        GameManager.instance.Can_move = false;
        FindObjectOfType<Player>().canvas.gameObject.SetActive(false);

        Inven_manager.instance.shop.gameObject.layer = 0;
        ran_count = Random.Range(4, 7);
        StartCoroutine(cammove(false));
        spwan_customer();

        choose_obj.SetActive(false);

        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector2(0.2f, -3f);
        player.transform.GetChild(0).gameObject.SetActive(false);
        player.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Button_End_sale()
    {
        GameManager.instance.Can_interact = true;
        GameManager.instance.Can_move = true;
        FindObjectOfType<Player>().canvas.gameObject.SetActive(true);

        StartCoroutine(cammove(true));

        Inven_manager.instance.shop.gameObject.layer = 9;
        choose_obj.SetActive(false);

        GameObject player = GameObject.Find("Player");
        player.transform.GetChild(0).gameObject.SetActive(true);
        player.transform.GetChild(1).gameObject.SetActive(true);
    }

    IEnumerator cammove(bool is_return)
    {
        if (is_return)
        {
            for (float i = Camera.main.orthographicSize; i > 4; i -= 0.01f)
            {
                Camera.main.orthographicSize = i;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            for (float i = Camera.main.orthographicSize; i < 5; i += 0.01f)
            {
                Camera.main.orthographicSize = i;
                yield return new WaitForSeconds(0.05f);
            }
        }
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
