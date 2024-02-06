using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public string cant_buy_string;
        public Sprite image;
        public Sprite[] ani;
    }
    public strings_[] strings;
    public float sprite_speed;
    private bool is_play;

    private void Start()
    {
        GetComponent<Interaction>().interact += ChatBoxStart;

        transform.parent.position = Inven_manager.instance.shop.transform.Find("Start_target").position;
        target = Inven_manager.instance.shop.transform.Find("target");
        End_target = Inven_manager.instance.shop.transform.Find("End_target");
        ran_text = UnityEngine.Random.Range(0, strings.Length);

        transform.parent.GetComponent<SpriteRenderer>().sprite = strings[ran_text].image;

    }

    private void Update()
    {
        if (!is_end)
        {
            if (transform.parent.position == target.position)
            {
                is_play = false;
            }
            else
            {
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, target.position, speed * Time.deltaTime);
                is_play = true;
                StartCoroutine(play());
            }
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
                is_play = true;
                StartCoroutine(play());
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, End_target.position, speed * Time.deltaTime);
            }
        }
    }

    IEnumerator play()
    {
        while (is_play)
        {
            for (int i = 0; i < strings[ran_text].ani.Length; i++)
            {
                Debug.Log("!!");
                transform.parent.GetComponent<SpriteRenderer>().sprite = strings[ran_text].ani[i];
                yield return new WaitForSeconds(sprite_speed);
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
            
            textMesh.text = strings[ran_text].cant_buy_string.Replace("(item)", buy_item.Item_Name);
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
