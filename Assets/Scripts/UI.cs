using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public void ScreenSettingApply()
    {
        GameManager GM = GameManager.instance;
        Screen.SetResolution(GM.Width[GM.NowResolution], GM.Height[GM.NowResolution], GM.WindowScreen);
    }
    public void FullScreen(TextMeshProUGUI text)
    {
        GameManager.instance.WindowScreen = !GameManager.instance.WindowScreen;
        if (GameManager.instance.WindowScreen)
            text.text = "전체화면";
        else
            text.text = "창 모드";
    }
    public void ResolutionSetting(int Value)
    {
        GameManager.instance.NowResolution += (3 + Value);
        GameManager.instance.NowResolution %= 3;
    }
    public void ResolutionTextChange(TextMeshProUGUI text)
    {
        GameManager GM = GameManager.instance;
        text.text = GM.Width[GM.NowResolution].ToString() + "X" + GM.Height[GM.NowResolution].ToString();
    }
}