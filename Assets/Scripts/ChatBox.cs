using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    [SerializeField] string[] strings = new string[0];
    [SerializeField] float Xvalue;
    [SerializeField] float Yvalue;
    public GameObject ChatBoxPrefab;
    GameObject OBJ;
    TextMeshPro textMesh;
    int Count = 0;
    private void Start()
    {
        if (TryGetComponent(out Interaction interaction))
            interaction.interact += ChatBoxStart;
    }
    public void ChatBoxStart()
    {
        StopAllCoroutines();
        StartCoroutine(Disable());
        if (Count == 0)
        {
            OBJ = Instantiate(ChatBoxPrefab, transform.position + new Vector3(Xvalue, Yvalue, 0), Quaternion.identity);
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
        yield return new WaitForSeconds(3f);
        Destroy(OBJ);
        Count = 0;
    }
}
