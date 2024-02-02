using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public delegate void Interact();
    public Interact interact;
    [SerializeField] TextMeshPro text;
    public void InteractStart()
    {
        if (interact != null)
        {
            if (text != null)
                text.enabled = false;
            interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (text == null)
                return;

            text.text = GameManager.instance.OperationKey["Interaction"].ToString();
            text.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (text == null)
                return;

            text.enabled = false;
        }
    }
}
