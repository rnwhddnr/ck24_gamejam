using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu (fileName = "New Item", menuName = "Scriptable Object/Creat New Item")]

public class item : ScriptableObject
{
    public string Item_Name;
    [TextArea(3, 5)] public string Item_Description;
    public Image Item_Icon;
    public int coin;
}
