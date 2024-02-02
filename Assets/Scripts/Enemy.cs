using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject Item;
    [Space(10f)]
    public float move_speed;
   
    private float next_move;
    private Rigidbody2D rigid;
    public int hp;
    public int Attack = 1;
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
    [SerializeField] bool CommonMob = true;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        Choose_next_move();
        if (CommonMob)
            StartCoroutine(CommonMobAI());
    }
    IEnumerator CommonMobAI()
    {
        while (true) 
        {
            yield return null;
            Move();
        }
        //보스도Enemy.cs의 스탯들좀 쓰기위해 분리시켰어요.
    }
    /*
    private void FixedUpdate()
    {
        Move();
    }
    */
    private void Destroy_enemy()
    {
        Instantiate(Item, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.transform.root.GetComponent<Player>();
            HP -= player.Atk;
            player.PolygonCollider.enabled = false;
            Debug.Log("HIT");
        }
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
