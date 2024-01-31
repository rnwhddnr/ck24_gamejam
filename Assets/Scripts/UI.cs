using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public void SceneLoad(string Name)
    {
        SceneManager.LoadScene(Name);
    }
    public void OnOff(GameObject OBJ)
    {
        OBJ.SetActive(!OBJ.activeSelf);
    }
    public void GameExit()
    {
        Application.Quit();
    }
}