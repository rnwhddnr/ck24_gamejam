using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadScreen : MonoBehaviour
{
    RectTransform RT;
    Scrollbar SB;
    private void Start()
    {
        RT = transform.GetChild(0).GetComponent<RectTransform>();
        SB = transform.GetChild(1).GetComponent<Scrollbar>();
    }
    void Update()
    {
        Scroll();
        SB.value = RT.anchoredPosition.y / 1900f;
    }
    void Scroll()
    {
        float scoll = Input.GetAxis("Mouse ScrollWheel");
        if (scoll > 0)
        {
            if (RT.anchoredPosition.y <= 0)
            {
                RT.anchoredPosition = new Vector2(0, 0);
                return;
            }
            else
            {
                RT.anchoredPosition -= new Vector2(0, 500 * scoll);
            }
        }
        else if (scoll < 0)
        {
            if (RT.anchoredPosition.y >= 1900)
            {
                return;
            }
            else
            {
                RT.anchoredPosition -= new Vector2(0, 500 * scoll);
            }
        }
    }
    public void ScrollBar()
    {
        RT.anchoredPosition = new Vector2(0, 1900f * SB.value);
    }
}
