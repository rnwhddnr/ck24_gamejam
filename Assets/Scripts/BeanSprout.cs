using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanSprout : MonoBehaviour
{
    Enemy Enemy;
    [SerializeField] Sprite DefaultSprite;
    [SerializeField] Sprite[] AttackSprite;
    [SerializeField] int StartSprite;
    [SerializeField] int EndSprite;
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
        Enemy.rigid.velocity = new Vector2(0, 0);
        for (int i = 0; i < AttackSprite.Length; i++)
        {
            yield return new WaitForSeconds(AttackTime / AttackSprite.Length);
            if (i == StartSprite)
                Battle.size *= 2f;
            if (i == EndSprite)
                Battle.size *= 0.5f;
            Enemy.SR.sprite = AttackSprite[i];
        }
        Enemy.SR.sprite = DefaultSprite;
        Enemy.Attacked = false;
    }
}
