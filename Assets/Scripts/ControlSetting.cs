using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlSetting : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] ControlText;
    [SerializeField] string[] Keyname;
    [SerializeField] GameObject BackGround;
    TextMeshProUGUI text;
    private void Start()
    {
        for(int i = 0; i < ControlText.Length; i++)
        {
            ControlText[i].text = GameManager.instance.OperationKey[Keyname[i]].ToString();
        }
    }
    public void KeyChange(string KeyName)
    {
        StartCoroutine(WaitPress(KeyName));
    }
    public void TextSelect(TextMeshProUGUI TMP)
    {
        text = TMP;
    }
    IEnumerator WaitPress(string KeyName)
    {
        BackGround.SetActive(true);
        while (true)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        for (int i = 0; i < Keyname.Length; i++)
                        {
                            if (GameManager.instance.OperationKey[Keyname[i]] == key)
                            {
                                BackGround.SetActive(false);
                                yield break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        GameManager.instance.OperationKey[KeyName] = key;
                        text.text = key.ToString();
                        BackGround.SetActive(false);
                        GameManager.instance.KeySave();
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }
}
