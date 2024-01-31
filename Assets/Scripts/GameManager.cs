using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<string, KeyCode> OperationKey = new Dictionary<string, KeyCode>();
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        OperationKey.Add("RightMove", KeyCode.D);
        OperationKey.Add("LeftMove", KeyCode.A);
        OperationKey.Add("Jump", KeyCode.W);
        OperationKey.Add("Sit", KeyCode.S);
        OperationKey.Add("Skill", KeyCode.E);
        OperationKey.Add("Inventory", KeyCode.C);
        OperationKey.Add("ZoomMap", KeyCode.Tab);
        OperationKey.Add("Interaction", KeyCode.F);
    }
    void Start()
    {
        SceneManager.LoadScene("Main");
    }
}