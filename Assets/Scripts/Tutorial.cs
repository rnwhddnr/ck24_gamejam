using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] bool skip;

    [SerializeField] Camera mainCamera;
    [SerializeField] Camera TutoCamera;
    [SerializeField] GameObject Portal;
    [SerializeField] Interaction[] NPC = new Interaction[4];
    [SerializeField] Player Player;
    [SerializeField] GameObject ChatBoxPrefab;
    [SerializeField] GameObject Camerapoint;
    ChatBox ChatBox;
    private void Start()
    {
        if (skip)
        {
            Skip();
            return;
        }

        ChatBox = GetComponent<ChatBox>();
        if (GameManager.instance.Data.Tutorial)
            Destroy(gameObject);
        else
            StartCoroutine(StartTutorial());
    }
    void Skip()
    {
        for (int i = 0; i < NPC.Length; i++)
        {
            Destroy(NPC[i].transform.parent.GetChild(1).gameObject);
            Destroy(NPC[i].transform.parent.GetChild(0).gameObject);
        }
        GameManager.instance.Can_interact = true;
        mainCamera.enabled = true;
        TutoCamera.enabled = false;
        GameManager.instance.Data.Tutorial = true;
        Destroy(gameObject);
    }
    IEnumerator StartTutorial()
    {
        GameManager.instance.Can_interact = false;
        Player.transform.GetChild(1).transform.gameObject.SetActive(false);
        Portal.SetActive(false);
        mainCamera.enabled = false;
        TutoCamera.enabled = true;
        TutoCamera.transform.position = new Vector3(8.5f, Camerapoint.transform.position.y, -10);
        Player.Interactable = false;
        while (Vector3.Distance(TutoCamera.transform.position, new Vector3(Camerapoint.transform.position.x, Camerapoint.transform.position.y, -10)) > 0.1f)
        {
            yield return null;
            TutoCamera.transform.position = Vector3.Lerp(TutoCamera.transform.position, new Vector3(Camerapoint.transform.position.x, Camerapoint.transform.position.y, -10), 10f * Time.deltaTime);
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        StartCoroutine(CameraShake(TutoCamera, 0.05f, 1f, TutoCamera.transform.position));
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        StartCoroutine(CameraShake(TutoCamera, 0.08f, 1f, TutoCamera.transform.position));
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        yield return new WaitForSeconds(1f);
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        yield return new WaitForSeconds(1f);
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        yield return new WaitForSeconds(1f);
        StartCoroutine(CameraShake(TutoCamera, 0.12f, 1.5f, TutoCamera.transform.position));
        yield return new WaitForSeconds(1.5f);
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);//��Ż ����
        Portal.SetActive(true);
        yield return new WaitForSeconds(1f);
        ChatBox.OBJ.SetActive(false);
        while (Vector3.Distance(Player.transform.position, new Vector3(-9, -3.28f, -0)) > 0.1f)//�̵�
        {
            yield return null;
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, new Vector3(-9, -3.28f, -0), 5 * Time.deltaTime);
            TutoCamera.transform.position = Vector3.Lerp(TutoCamera.transform.position, new Vector3(-1, Camerapoint.transform.position.y, -10), 3f * Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);
        ChatBox.OBJ.SetActive(true);
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);//���Թ���
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 3);//�� ����
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        NPC[3].interact();
        yield return new WaitForSeconds(4);
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(NPC[3].transform.parent.GetChild(1).gameObject);
        Destroy(NPC[3].transform.parent.GetChild(0).gameObject);
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);//�̰Ų�?
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        Player.transform.GetChild(1).transform.gameObject.SetActive(true);
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        ChatBox.Chat(Player.transform.position + new Vector3(0, 2.5f) - transform.position);
        GameManager.instance.Can_interact = true;
        mainCamera.enabled = true;
        TutoCamera.enabled = false;
        GameObject OBJ = Instantiate(ChatBoxPrefab);
        TextMeshPro text = OBJ.GetComponent<TextMeshPro>();
        text.text = GameManager.instance.OperationKey["Jump"].ToString() + " �� ���� ����.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Jump"]))
            {
                break;
            }
        }
        text.text = GameManager.instance.OperationKey["LeftMove"].ToString() + " �� ���� ���� �̵�.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["LeftMove"]))
            {
                break;
            }
        }
        text.text = GameManager.instance.OperationKey["RightMove"].ToString() + " �� ���� ������ �̵�.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["RightMove"]))
            {
                break;
            }
        }
        text.text = GameManager.instance.OperationKey["Inventory"].ToString() + " �� ���� �κ��丮 ����.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Inventory"]))
            {
                break;
            }
        }
        text.text = GameManager.instance.OperationKey["Interaction"].ToString() + " �� ���� ��ȣ�ۿ�.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetKeyDown(GameManager.instance.OperationKey["Interaction"]))
            {
                break;
            }
        }
        text.text = "���콺 ��Ŭ������ ���� ������.";
        while (true)
        {
            yield return null;
            OBJ.transform.position = Player.transform.position + Vector3.up * 2;
            if (Input.GetMouseButtonDown(1))
            {
                break;
            }
        }
        text.text = "���콺 ��Ŭ������ ����.";
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
    IEnumerator CameraShake(Camera camera, float Power, float ShakeTime, Vector3 BeforePos)
    {
        float T = 0;
        while (T < ShakeTime)
        {
            T += Time.deltaTime;
            camera.transform.position = Random.insideUnitSphere * Power + BeforePos;
            yield return null;
        }
        camera.transform.position = BeforePos;
    }
}
