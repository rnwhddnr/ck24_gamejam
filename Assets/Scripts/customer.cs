using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class customer : MonoBehaviour
{
    public Transform target;
    public Transform End_target;
    private bool is_end;
    public item buy_item;

    [SerializeField] float speed;
    [SerializeField] float Yvalue;
    public GameObject ChatBoxPrefab;
    GameObject OBJ;
    TextMeshPro textMesh;
    [SerializeField] int Count = 0;
    [SerializeField] int ran_text;

    [Serializable]
    public struct strings_
    {
        public string[] text;
    }
    public strings_[] strings;
    public strings_[] cant_buy_string;

    private void Start()
    {
        GetComponent<Interaction>().interact += ChatBoxStart;

        transform.parent.position = Inven_manager.instance.shop.transform.Find("Start_target").position;
        target = Inven_manager.instance.shop.transform.Find("target");
        End_target = Inven_manager.instance.shop.transform.Find("End_target");
        ran_text = UnityEngine.Random.Range(0, strings.Length);
    }

    private void Update()
    {
        if (!is_end)
        {
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            if (transform.parent.position == End_target.position)
            {
                Inven_manager.instance.shop.GetComponentInChildren<Shop_interact>().spwan_customer();
                Destroy(transform.parent.gameObject);
            }
            else
            {
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, End_target.position, speed * Time.deltaTime);
            }
        }
    }

    public void ChatBoxStart()
    {
        if (Count == 0)
        {
            //OBJ = Instantiate(ChatBoxPrefab, transform.position + new Vector3(0, Yvalue, 0), Quaternion.identity);
            OBJ = transform.parent.Find("ChatBox").gameObject;
            textMesh = OBJ.GetComponent<TextMeshPro>();
        }

        if (!Inven_manager.instance.shop.Food_items.Contains(buy_item))
        {
            textMesh.enabled = true;
            cant_buy_string[ran_text].text[0] = cant_buy_string[ran_text].text[0].Replace("(item)", buy_item.Item_Name);
            textMesh.text = cant_buy_string[ran_text].text[0];
            is_end = true;
            return;
        }

        if (Count == strings.Length)
        {
            Inven_manager.instance.shop.sell_food(buy_item);
            is_end = true;
            return;
        }
        strings[ran_text].text[Count] = strings[ran_text].text[Count].Replace("(item)", buy_item.Item_Name);

        textMesh.enabled = true;
        textMesh.text = strings[ran_text].text[Count];
        Count++;
    }
}
