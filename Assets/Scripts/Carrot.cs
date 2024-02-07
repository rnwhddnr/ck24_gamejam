using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    Enemy Enemy;
    [SerializeField] Sprite DefaultSprite;
    [SerializeField] Sprite[] ChargeSprite;
    [SerializeField] float ChargeTime;
    [SerializeField] Sprite[] DashSprite;
    [SerializeField] float DashTime;
    [SerializeField] Sprite[] UpSprite;
    [SerializeField] float UpTime;
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        Enemy.Go = ChargeStart;
    }
    public void ChargeStart()
    {
        Enemy.player = null;
        Enemy.Attacked = true;
        StartCoroutine(Charge());
    }
    IEnumerator Charge()
    {
        Enemy.CanMove = false;
        Enemy.rigid.velocity = new Vector2(0, 0);
        for (int i = 0; i < ChargeSprite.Length; i++)
        {
            yield return new WaitForSeconds(ChargeTime / ChargeSprite.Length);
            Enemy.SR.sprite = ChargeSprite[i];
        }
        StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        float XDir = 0;
        if (transform.eulerAngles.y == 180)
            XDir = 1;
        else if (transform.eulerAngles.y == 0)
            XDir = -1;
        Enemy.rigid.velocity = new Vector2(XDir * Enemy.move_speed * 5f, Enemy.rigid.velocity.y);
        int Count = DashSprite.Length * 3;
        for (int i = 0; i < Count; i++)
        {
            yield return new WaitForSeconds(ChargeTime / Count);
            Enemy.SR.sprite = DashSprite[i % DashSprite.Length];
        }
        Enemy.rigid.velocity = new Vector2(0, Enemy.rigid.velocity.y);
        StartCoroutine(Up());
    }
    IEnumerator Up()
    {
        Enemy.rigid.velocity = new Vector2(0, 0);
        for (int i = 0; i < UpSprite.Length; i++)
        {
            yield return new WaitForSeconds(UpTime / UpSprite.Length);
            Enemy.SR.sprite = UpSprite[i];
        }
        Enemy.SR.sprite = DefaultSprite;
        Enemy.CanMove = true;
        Enemy.Attacked = false;
    }
}