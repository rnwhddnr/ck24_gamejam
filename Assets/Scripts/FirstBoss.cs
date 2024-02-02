using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : MonoBehaviour
{
    Player player;
    Enemy Enemy;
    int AttackDir;
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Start()
    {
        StartCoroutine(First());
    }
    IEnumerator First()
    {
        while (Vector3.Distance(transform.position, player.transform.position + Vector3.up * 3) > 0.1f)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + Vector3.up * 3, Enemy.move_speed * Time.deltaTime);
        }
        int i = Random.Range(0, 1);
        if (i == 1)
            AttackDir = 1;
        else
            AttackDir = -1;
        StartCoroutine(Second());
    }
    IEnumerator Second()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(Third());
    }
    IEnumerator Third()
    {
        Vector3 Target = player.transform.position + (Vector3.left * AttackDir * 3);
        while (Vector3.Distance(transform.position, Target) > 0.1f)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, Target, Enemy.move_speed * Time.deltaTime);
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Fourth());
    }
    IEnumerator Fourth()
    {
        Vector2 Dir = player.transform.position - transform.position;
        int dir = System.Math.Sign(Dir.x);
        Vector3 Target = player.transform.position + (Vector3.left * (-dir) * 3);
        while (Vector3.Distance(transform.position, Target) > 0.01f)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, Target, Enemy.move_speed * Time.deltaTime * 2f);
        }
        yield return new WaitForSeconds(3);
        StartCoroutine (First());
    }
}
