using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turorial : MonoBehaviour
{
    [SerializeField] bool skip;

    [SerializeField] Camera mainCamera;
    [SerializeField] Camera TutoCamera;
    [SerializeField] GameObject Portal;
    [SerializeField] Interaction[] NPC = new Interaction[4];
    [SerializeField] Player Player;
    [SerializeField] GameObject ChatBoxPrefab;
    ChatBox ChatBox;
    private void Start()
    {
        if (skip)
            return;

        ChatBox = GetComponent<ChatBox>();
        if (GameManager.instance.Data.Tutorial)
            Destroy(gameObject);
        else
            StartCoroutine(StartTutorial());
    }
    IEnumerator StartTutorial()
    {
        GameManager.instance.Can_interact = false;
        Player.transform.GetChild(1).transform.gameObject.SetActive(false);
        Portal.SetActive(false);
        mainCamera.enabled = false;
        TutoCamera.enabled = true;
        TutoCamera.transform.position = new Vector3(8.5f, 0.6f, -10);
        while (Vector3.Distance(TutoCamera.transform.position, new Vector3(-11, 0.6f, -10)) > 0.1f)
        {
            yield return null;
            TutoCamera.transform.position = Vector3.Lerp(TutoCamera.transform.position, new Vector3(-11, 0.6f, -10), 0.5f * Time.deltaTime);
        }
        Portal.SetActive(true);
        yield return new WaitForSeconds(1);
        NPC[0].interact();
        yield return new WaitForSeconds(0.4f);
        NPC[1].interact();
        yield return new WaitForSeconds(0.6f);
        NPC[2].interact();
        NPC[3].interact();
        yield return new WaitForSeconds(1);
        NPC[0].interact();
        NPC[1].interact();
        yield return new WaitForSeconds(0.4f);
        NPC[2].interact();
        yield return new WaitForSeconds(0.4f);
        NPC[3].interact();
        yield return new WaitForSeconds(0.4f);
        NPC[0].interact();
        NPC[1].interact();
        NPC[2].interact();
        NPC[3].interact();
        for(int i=0; i<NPC.Length; i++)
        {
            Destroy(NPC[i].transform.parent.GetChild(1).gameObject);
            Destroy(NPC[i].transform.parent.GetChild(0).gameObject);
        }
        yield return new WaitForSeconds(6);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 3);
        yield return new WaitForSeconds(5);
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        ChatBox.ChatBoxStart();
        yield return new WaitForSeconds(2);
        ChatBox.ChatBoxStart();
        yield return new WaitForSeconds(1.5f);
        ChatBox.ChatBoxStart();
        yield return new WaitForSeconds(1);
        ChatBox.ChatBoxStart();
        yield return new WaitForSeconds(1);
        Player.transform.GetChild(1).transform.gameObject.SetActive(true);
        GameManager.instance.Can_interact = true;
        mainCamera.enabled = true;
        TutoCamera.enabled = false;
        GameObject OBJ = Instantiate(ChatBoxPrefab);
        TextMeshPro text = OBJ.GetComponent<TextMeshPro>();
        text.text = GameManager.instance.OperationKey["Jump"].ToString() + " 를 눌러 점프.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Jump"]))
            {
                break;
            }
        }
        text.text = GameManager.instance.OperationKey["LeftMove"].ToString() + " 를 눌러 왼쪽 이동.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["LeftMove"]))
            {
                break;
            }
        }
        text.text = GameManager.instance.OperationKey["RightMove"].ToString() + " 를 눌러 오른쪽 이동.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["RightMove"]))
            {
                break;
            }
        }
        text.text = GameManager.instance.OperationKey["Inventory"].ToString() + " 를 눌러 인벤토리 열기.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Inventory"]))
            {
                break;
            }
        }
        text.text = GameManager.instance.OperationKey["Interaction"].ToString() + " 를 눌러 상호작용.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        text.text = "마우스 우클릭으로 눌러 구르기.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetMouseButtonDown(1))
            {
                break;
            }
        }
        text.text = "마우스 좌클릭으로 공격.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
        }
        Destroy(OBJ);
        GameManager.instance.Data.Tutorial = true;
        yield return null;
    }
}
