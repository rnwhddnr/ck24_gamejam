using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField] GameObject SaveUI;
    void Start()
    {
        GetComponent<Interaction>().interact += save;
    }
    void save()
    {
        SaveUI.SetActive(!SaveUI.activeSelf);
    }
    public void SaveFile(int SaveNumber)
    {
        GameManager.instance.SaveFileNum = SaveNumber;
        GameManager.instance.SaveData();
    }
}