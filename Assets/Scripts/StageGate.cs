using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StageGate : MonoBehaviour
{
    [SerializeField] GameObject[] StageMonster = new GameObject[0];
    [SerializeField] Vector2[] Pos = new Vector2[0];
    private void Start()
    {
        GetComponent<Interaction>().interact += SummonStageMonster;
    }
    void SummonStageMonster()
    {
        for(int i = 0; i < StageMonster.Length; i++)
        {
            Instantiate(StageMonster[i], Pos[i], Quaternion.identity);
        }
        Destroy(transform.parent.gameObject);
    }
}