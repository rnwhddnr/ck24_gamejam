using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up_shop_interact : MonoBehaviour
{
    [SerializeField] GameObject UI;

    private void Start()
    {
        GetComponent<Interaction>().interact += active_UI;
    }

    public void active_UI()
    {
        GameManager.instance.Can_interact = false;
        UI.SetActive(true);
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
            default:
                if (Inven_manager.instance.Coin - 50 >= 0)
                    Inven_manager.instance.Coin -= 50;
                else
                    Debug.Log("코인부족");
                break;
        }
    }
}
