using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public item item;
    [Space(10f)]
    public float move_speed;
   
    private float next_move;
    private Rigidbody2D rigid;
    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            if (hp != value)
            {
                if (value <= 0)
                    Destroy_enemy();
                else
                    hp = value;
            }
        }

    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        Choose_next_move();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Destroy_enemy()
    {
        Inven_manager.instance.Add_new_item(item);
        Destroy(gameObject);
    }

    private void Move()
    {
        rigid.velocity = new Vector2(next_move * move_speed, rigid.velocity.y);

        Vector2 front = new Vector2(rigid.position.x + next_move, rigid.position.y);
        RaycastHit2D rayhit = Physics2D.Raycast(front, Vector2.down, 1, LayerMask.GetMask("Block"));
        Debug.DrawRay(front, Vector2.down, Color.green);

        if (rayhit.collider == null)
            next_move *= -1;
    }

    private void Choose_next_move()
    {
        next_move = Random.Range(-1, 2);

        if (next_move == 0)
            Choose_next_move();
    }
}
