using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "New Recipe", menuName = "Scriptable Object/Creat New Recipe")]

public class Recipe : ScriptableObject
{
    public string Recipe_Name;
    [TextArea(3, 5)] public string Recipe_Description;
    [Space(10f)]
    public item[] input_items = new item[0];
    public item output_item;

    public item Get_item(item[] items)
    {
        input_items = input_items.OrderBy(x => x.Item_Name).ToArray();
        items = items.OrderBy(x => x.Item_Name).ToArray();

        if (items.SequenceEqual(input_items))
            return output_item;
        else
            return null;
    }
}