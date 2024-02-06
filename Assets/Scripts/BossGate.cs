using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossGate : MonoBehaviour
{
    [SerializeField] Camera MainCamera;
    [SerializeField] Camera CutSceneCamera;
    [SerializeField] GameObject cutScene;
    [SerializeField] Animator Animator;
    [SerializeField] Canvas canvas;
    [SerializeField]Player player;
    private void Start()
    {
        GetComponent<Interaction>().interact += CutSceneStart;
    }
    void CutSceneStart()
    {
        StartCoroutine(CutScene());
    }
    IEnumerator CutScene()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Animator.SetTrigger("Start");
        MainCamera.enabled = false;
        CutSceneCamera.enabled = true;
        player.CameraFollow = false;
        player.Camera = CutSceneCamera;
        GameManager.instance.StartCoroutine(GameManager.instance.CameraShake(CutSceneCamera, 0.15f, 4.5f, CutSceneCamera.transform.position));
        while(true)
        {
            yield return null;
            if (Animator.GetBool("GroundAttack"))
                break;
        }
        GameManager.instance.StartCoroutine(GameManager.instance.CameraShake(CutSceneCamera, 0.5f, 0.3f, CutSceneCamera.transform.position));
        while (true)
        {
            yield return null;
            if (Animator.GetBool("End"))
                break;
        }
        canvas.worldCamera = CutSceneCamera;
        CutSceneCamera.transform.GetChild(0).gameObject.SetActive(false);
        CutSceneCamera.transform.GetChild(1).gameObject.SetActive(false);
    }
}