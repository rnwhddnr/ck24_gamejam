using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject Item;
    public GameObject Center;

    public bool Attacked;
    public float AttackRange;
    public SpriteRenderer SR;
    public delegate void AttackStart();
    public AttackStart Go;
    [Space(10f)]
    public float move_speed;
    [SerializeField] Transform[] move_pos;
    private bool is_return;

    public Rigidbody2D rigid;
    public int hp;
    public int Attack = 1;
    public Player player;
    public int Dir;

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
        SR = GetComponent<SpriteRenderer>();
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
        if (Item != null)
        {
            GameObject item = Instantiate(Item, transform.position, Quaternion.identity);
            item.GetComponent<SpriteRenderer>().sprite = item.GetComponent<ItemOBJ>().item.Item_Icon;
        }
        Destroy(gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Detect"))
            player = null;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Detect") && !Attacked)
            player = collision.transform.root.GetComponent<Player>();
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
        if (!is_return)
        {
            transform.position = Vector2.MoveTowards(transform.position, move_pos[0].position, move_speed);
            SR.flipX = false;

            if (transform.position == move_pos[0].position)
                is_return = true;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, move_pos[1].position, move_speed);
            SR.flipX = true;
            
            if (transform.position == move_pos[1].position)
                is_return = false;
        }


        if (player != null)
        {
            Dir = System.Math.Sign((player.transform.position - transform.position).x);
            if (Dir == 1)
                transform.eulerAngles = new Vector3(0, 180, 0);
            else if (Dir == -1)
                transform.eulerAngles = new Vector3(0, 0, 0);
            rigid.velocity = new Vector2(Dir * move_speed, rigid.velocity.y);

            Vector2 front = new Vector2(Center.transform.position.x + Dir, Center.transform.position.y);
            RaycastHit2D rayhit = Physics2D.Raycast(front, Vector2.down, 1, LayerMask.GetMask("Block"));
            Debug.DrawRay(front, Vector2.down, Color.green);

            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= AttackRange && Go != null)
                Go();
            if (rayhit.collider == null)
                rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }
}
