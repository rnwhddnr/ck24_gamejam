using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScreen : MonoBehaviour
{
    RectTransform RT;
    Scrollbar SB;
    [SerializeField] float Max;
    [SerializeField] float Min;
    [SerializeField] TextMeshProUGUI[] TMP = new TextMeshProUGUI[10];
    private void Start()
    {
        RT = transform.GetChild(0).GetComponent<RectTransform>();
        SB = transform.GetChild(1).GetComponent<Scrollbar>();
    }
    private void OnEnable()
    {
        RefreshSaveButtonText();
    }
    public void RefreshSaveButtonText()
    {
        if (gameObject.name == "SaveLoadScreen")
        {
            for (int i = 0; i < 10; i++)
            {
                string path = Path.Combine(Application.dataPath, "Data_" + i.ToString() + ".json");
                if (!File.Exists(path))
                {
                    TMP[i].text = "세이브 파일이 없습니다.";
                }
                else
                {
                    TMP[i].text = "세이브 파일 " + i.ToString();
                }
            }
        }
    }
    void Update()
    {
        Scroll();
        SB.value = RT.anchoredPosition.y / Max;
    }
    void Scroll()
    {
        float scoll = Input.GetAxis("Mouse ScrollWheel");
        if (scoll > 0)
        {
            if (RT.anchoredPosition.y <= Min)
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
            if (RT.anchoredPosition.y >= Max)
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
        RT.anchoredPosition = new Vector2(0, Max * SB.value);
    }
}
