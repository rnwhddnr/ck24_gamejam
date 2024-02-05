using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOBJ : MonoBehaviour
{
    public item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Inven_manager.instance.Add_new_item(item);
            Destroy(gameObject);
        }
    }
}
