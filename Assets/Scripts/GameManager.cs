using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Data
{
    public bool Tutorial;
    public int Coin;
    public string SceneName = "Main";
    Inven_manager inven;
}
[System.Serializable]
public class KeySaveFile
{
    public List<string> Keys = new List<string>();
    public List<KeyCode> KeyCodes = new List<KeyCode>();
}
public class GameManager : MonoBehaviour
{
    public Data Data;
    public int SaveFileNum;
    public static GameManager instance;
    public Dictionary<string, KeyCode> OperationKey = new Dictionary<string, KeyCode>();
    public int[] Width = { 1920, 1600, 1280 };
    public int[] Height = { 1080, 900, 800 };
    public int NowResolution;
    public bool WindowScreen = true;
    public bool Can_interact = true;
    public bool Can_move = true;

    private void Awake()
    {
        KeySaveFile saveFile = new KeySaveFile();
        string path = Path.Combine(Application.dataPath, "KeyData.json");
        Data = new Data();
        instance = this;
        DontDestroyOnLoad(gameObject);
        if (!File.Exists(path))
        {
            OperationKey.Add("RightMove", KeyCode.D);
            OperationKey.Add("LeftMove", KeyCode.A);
            OperationKey.Add("Jump", KeyCode.W);
            OperationKey.Add("Sit", KeyCode.S);
            OperationKey.Add("Skill", KeyCode.E);
            OperationKey.Add("Inventory", KeyCode.C);
            OperationKey.Add("Interaction", KeyCode.F);
            foreach (KeyValuePair<string, KeyCode> S in OperationKey)
            {
                saveFile.KeyCodes.Add(S.Value);
                saveFile.Keys.Add(S.Key);
            }
            string jsonData = JsonUtility.ToJson(saveFile);
            File.WriteAllText(path, jsonData);
        }
        else
        {
            string jsonData = File.ReadAllText(path);
            saveFile = JsonUtility.FromJson<KeySaveFile>(jsonData);
            for (int i = 0; i < saveFile.KeyCodes.Count; i++)
            {
                OperationKey.Add(saveFile.Keys[i], saveFile.KeyCodes[i]);
            }
        }
    }
    public void KeySave()
    {
        KeySaveFile saveFile = new KeySaveFile();
        string path = Path.Combine(Application.dataPath, "KeyData.json");
        foreach (KeyValuePair<string, KeyCode> S in OperationKey)
        {
            saveFile.KeyCodes.Add(S.Value);
            saveFile.Keys.Add(S.Key);
        }
        string jsonData = JsonUtility.ToJson(saveFile);
        File.WriteAllText(path, jsonData);
    }
    void Start()
    {
        SceneManager.LoadScene("Main");
    }
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(Data);
        string path = Path.Combine(Application.dataPath, "Data_" + SaveFileNum.ToString() + ".json");
        File.WriteAllText(path, jsonData);
    }
    public void LoadData()
    {
        string path = Path.Combine(Application.dataPath, "Data_" + SaveFileNum.ToString() + ".json");
        if (!File.Exists(path))
        {
            SaveData();
        }
        else
        {
            string jsonData = File.ReadAllText(path);
            Data = JsonUtility.FromJson<Data>(jsonData);
            SceneManager.LoadScene(Data.SceneName);
        }
    }
}