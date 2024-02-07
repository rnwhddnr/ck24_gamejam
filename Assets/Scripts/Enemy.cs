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
    [SerializeField] float[] move_pos = new float[2];
    Vector2 StartPosition;
    bool CheckPlayer;
    private bool is_return;
    public bool CanMove = true;

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
        StartPosition = transform.position;
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
            if (CanMove)
                Move();
        }
    }
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
        {
            player = null;
            StartCoroutine(checkPlayer());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Detect") && !Attacked)
        {
            player = collision.transform.root.GetComponent<Player>();
            CheckPlayer = true;
        }
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
        if (player != null)
        {
            Dir = System.Math.Sign((player.transform.position - transform.position).x);
            if (Dir == 1)
                transform.eulerAngles = new Vector3(0, 180, 0);
            else if (Dir == -1)
                transform.eulerAngles = new Vector3(0, 0, 0);
            rigid.velocity = new Vector2(Dir * move_speed, rigid.velocity.y);

            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= AttackRange && Go != null)
                Go();
        }
        else if(!CheckPlayer)
        {
            if (move_pos.Length == 0)
                return;
            if (!is_return)
            {
                rigid.velocity = new Vector2(move_speed * -1, rigid.velocity.y);
                transform.eulerAngles = new Vector3(0, 0, 0);

                if (Vector2.Distance(transform.position, new Vector2(StartPosition.x+move_pos[0], transform.position.y)) < 0.05f)
                    is_return = true;
            }
            else
            {
                rigid.velocity = new Vector2(move_speed, rigid.velocity.y);
                transform.eulerAngles = new Vector3(0, 180, 0);

                if (Vector2.Distance(transform.position, new Vector2(StartPosition.x + move_pos[1], transform.position.y)) < 0.05f)
                    is_return = false;
            }
        }
    }
    IEnumerator checkPlayer()
    {
        yield return new WaitForSeconds(3);
        CheckPlayer = false;
        if(move_pos.Length != 0)
            is_return = Vector2.Distance(transform.position, new Vector2(StartPosition.x + move_pos[1], transform.position.y)) > Vector2.Distance(transform.position, new Vector2(StartPosition.x + move_pos[0], transform.position.y));
    }
}
