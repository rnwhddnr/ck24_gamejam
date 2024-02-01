using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    [SerializeField] string[] strings = new string[0];
    [SerializeField] float Yvalue;
    public GameObject ChatBoxPrefab;
    GameObject OBJ;
    TextMeshPro textMesh;
    int Count = 0;
    private void Start()
    {
        GetComponent<Interaction>().interact += ChatBoxStart;
    }
    public void ChatBoxStart()
    {
        StopAllCoroutines();
        StartCoroutine(Disable());
        if (Count == 0)
        {
            OBJ = Instantiate(ChatBoxPrefab, transform.position + new Vector3(0, Yvalue, 0), Quaternion.identity);
            textMesh = OBJ.GetComponent<TextMeshPro>();
        }
        if (Count == strings.Length)
        {
            Destroy(OBJ);
            Count = 0;
            return;
        }
        textMesh.text = strings[Count];
        Count++;
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(3f);
        Destroy(OBJ);
        Count = 0;
    }
}