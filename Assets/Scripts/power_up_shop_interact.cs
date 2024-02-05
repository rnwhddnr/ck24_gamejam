using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class power_up_shop_interact : MonoBehaviour
{
    [SerializeField] GameObject UI;
    private Button[] buttons; 

    private void Start()
    {
        GetComponent<Interaction>().interact += active_UI;
        buttons = UI.transform.Find("Panel").GetComponentsInChildren<Button>();
    }

    public void active_UI()
    {
        GameManager.instance.Can_interact = false;
        UI.SetActive(true);

        if (Inven_manager.instance.Coin < 100)
        {
            foreach (Button B in buttons)
                B.interactable = false;
        }
        else
        {
            foreach (Button B in buttons)
                B.interactable = true;
        }
    }

    public void disable_UI()
    {
        GameManager.instance.Can_interact = true;
        UI.SetActive(false);
    }

    public void click_button(string name)
    {
        switch (name)
        {
            case "ATK":
                FindObjectOfType<Player>().Atk += 1;
                break;
            case "HP":
                FindObjectOfType<Player>().MaxHp += 1;
                break;
            case "SPEED":
                FindObjectOfType<Player>().Speed += 0.5f;
                break;

            default:
                if (Inven_manager.instance.Coin >= 100)
                {
                    foreach (Button B in buttons)
                        B.interactable = true;
                    Inven_manager.instance.Coin -= 100;
                    if (Inven_manager.instance.Coin < 100)
                    {
                        foreach (Button B in buttons)
                            B.interactable = false;
                    }
                }
                break;
        }
    }
}
