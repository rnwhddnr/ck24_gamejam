using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rice : MonoBehaviour
{
    Enemy Enemy;
    [SerializeField] Sprite DefaultSprite;
    [SerializeField] Sprite[] AttackSprite;
    [SerializeField] float AttackTime;
    [SerializeField] BoxCollider2D Battle;
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        Enemy.Go = AttackStart;
    }
    void AttackStart()
    {
        Enemy.player = null;
        Enemy.Attacked = true;
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        float XDir = 0;
        if (transform.eulerAngles.y == 180)
            XDir = 1;
        else if (transform.eulerAngles.y == 0)
            XDir = -1;
        Enemy.rigid.velocity = new Vector2(XDir * Enemy.move_speed * 5f, 4.5f);
        for (int i = 0; i < AttackSprite.Length; i++)
        {
            yield return new WaitForSeconds(AttackTime / AttackSprite.Length);
            Enemy.SR.sprite = AttackSprite[i];
        }
        yield return new WaitForSeconds(1);
        Enemy.SR.sprite = DefaultSprite;
        Enemy.Attacked = false;
    }
}
