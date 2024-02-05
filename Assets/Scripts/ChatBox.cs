using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    [SerializeField] string[] strings = new string[0];
    [SerializeField] float Xvalue;
    [SerializeField] float Yvalue;
    [SerializeField] float DisableTime = 3;
    public GameObject ChatBoxPrefab;
    public GameObject OBJ;
    TextMeshPro textMesh;
    int Count = 0;
    private void Start()
    {
        for (int i = 0; i < strings.Length; i++)
        {
            strings[i] = strings[i].Replace("\\n", "\n");
        }

        if (TryGetComponent(out Interaction interaction))
            interaction.interact += ChatBoxStart;
    }
    public void ChatBoxStart()
    {
        Chat(new Vector2(Xvalue, Yvalue));
    }
    public void Chat(Vector2 Pos)
    {
        StopAllCoroutines();
        if (DisableTime != -1)
            StartCoroutine(Disable());
        if (Count == 0)
        {
            OBJ = Instantiate(ChatBoxPrefab);
            textMesh = OBJ.GetComponent<TextMeshPro>();
        }
        if (Count == strings.Length)
        {
            Destroy(OBJ);
            Count = 0;
            return;
        }
        OBJ.transform.position = transform.position + new Vector3(Pos.x, Pos.y, 0);
        textMesh.text = strings[Count];
        Count++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(OBJ);
            Count = 0;
        }
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(DisableTime);
        Destroy(OBJ);
        Count = 0;
    }
}
