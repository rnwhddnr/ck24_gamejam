using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private bool Invincibility;
    public float InvincibilityTime;
    bool Knockback;

    public int JumpCount = 1;
    public int MaxJumpCount = 1;
    public int JumpPower = 5;
    private bool NowJump;
    
    public float Speed;
    [HideInInspector] public bool cant_cam;
    [HideInInspector] public bool freez;

    private int maxhp;
    public int MaxHp
    {
        get { return maxhp; }
        set
        {
            maxhp = value;
            maxhp_Refresh();
        }
    }
    private int hp;
    public int Atk;
    bool Attacked;

    bool Roll;
    
    public float camera_speed;

    [SerializeField] BoxCollider2D HitRange;
    [SerializeField] SpriteRenderer SR;
    public Canvas canvas;
    public Camera Camera;
    public bool CameraFollow = true;
    GameManager GM;
    Rigidbody2D RG;
    [SerializeField] GameObject WeaponCenter;
    public PolygonCollider2D PolygonCollider;
    Animator Animator;
    public bool Interactable = true;

    public int Hp
    {
        get { return hp; }
        set
        {
            if (hp != value)
            {
                hp = value;
                HpRefresh();
            }
        }
    }
    public List<GameObject> Hpbars = new List<GameObject>();
    public GameObject HpbarPrefab;
    private void Awake()
    {
        Camera = Camera.main;
        GM = GameManager.instance;
        RG = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        maxhp_Refresh();
        MaxHp = 10;
    }

    private void maxhp_Refresh()
    {
        while(Hpbars.Count > 0)
        {
            GameObject obj = Hpbars[0];
            Hpbars.RemoveAt(0);
            Destroy(obj);
        }
        for (int i = 0; i < MaxHp; i++)
        {
            GameObject OBJ = Instantiate(HpbarPrefab, canvas.transform);
            Hpbars.Add(OBJ);
            RectTransform RT = OBJ.GetComponent<RectTransform>();
            RT.anchoredPosition = new Vector2(-800 + 50 * i, -435);
        }
        Hp = MaxHp;
        hp = Hp;
    }

    private void OnEnable()
    {
        GameManager.instance.Data.SceneName = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if (!GM.Can_interact)
        {
            RG.velocity = Vector2.zero;
            RG.gravityScale = 0;
            if (!GM.Can_move)
            {
                Interact();
            }
            return;
        }
        RG.gravityScale = 1;

        //±¸¸£±â
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0, Vector2.down, 0.52f, 1 << 3);
        if (Input.GetMouseButtonDown(1) && !Roll && hit.collider != null)
            StartCoroutine(Rolling(Camera.ScreenToWorldPoint(Input.mousePosition)));
        if (Roll)
            return;
        Move();
        Interact();
        Attack();
    }
    private void LateUpdate()
    {
        if (CameraFollow)
            Camera_move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Block") && NowJump)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0, Vector2.down, 0.52f, 1 << 3);
            Debug.DrawRay(transform.position, Vector2.down * 0.52f, Color.red);
            if (hit.collider != null && hit.transform.CompareTag("Block"))
            {
                JumpCount = MaxJumpCount;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
            Hp = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (Invincibility)
                return;
            StartCoroutine(HitAnimation(collision.transform.parent.GetComponent<Enemy>()));
        }
    }

    
    IEnumerator Rolling(Vector3 Pos)
    {
        Roll = true;
        int dir = Math.Sign((Pos - transform.position).x);
        RG.velocity = new Vector2(Speed * 3f * dir, RG.velocity.y);
        HitRange.enabled = false;
        yield return new WaitForSeconds(0.25f);
        HitRange.enabled = true;
        RG.velocity = new Vector2(0, RG.velocity.y);
        yield return new WaitForSeconds(0.1f);
        Roll = false;
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !Attacked)
        {
            Animator.SetTrigger("Attack");
        }
    }
    void AttackStart()
    {
        Attacked = true;
        PolygonCollider.enabled = true;
    }
    void AttackEnd()
    {
        Attacked = false;
        PolygonCollider.enabled = false;
        Animator.SetTrigger("End");
    }
    void Move()
    {
        if (!Knockback)
            RG.velocity = new Vector2(0, RG.velocity.y);
        float XMove = 0;
        if (Input.GetKey(GM.OperationKey["RightMove"]))
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = Quaternion.identity;

            Animator.SetBool("is_move", true);

            XMove += 1 * Speed;
            //transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey(GM.OperationKey["LeftMove"]))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.rotation = Quaternion.identity;

            Animator.SetBool("is_move", true);

            XMove -= 1 * Speed;
            //transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
            Animator.SetBool("is_move", false);

        if (XMove != 0 && !Knockback)
            RG.velocity = new Vector2(XMove, RG.velocity.y);
        if (Input.GetKeyDown(GM.OperationKey["Jump"]))
        {
            if (JumpCount > 0)
            {
                RG.velocity += new Vector2(0, JumpPower);
                JumpCount--;
                NowJump = true;

                Animator.SetTrigger("jump");
            }
        }
    }
    void Interact()
    {
        if (Inven_manager.instance.inven.activeSelf)
            return;

        if (Input.GetKeyDown(GM.OperationKey["Interaction"]))
        {
            List<float> Distance = new List<float>();
            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(2, 2), 0, transform.forward, 1, 1 << 9);
            if (hits.Length <= 0)
                return;
            for (int i = 0; i < hits.Length; i++)
            {
                Distance.Add(Vector2.Distance(transform.position, hits[i].transform.position));
            }
            int Index = Distance.IndexOf(Distance.Min());

            if (hits[Index].transform != null)
                hits[Index].transform.GetChild(0).GetComponent<Interaction>().InteractStart();
        }
    }
    void HpRefresh()
    {
        for (int i = 0; i < MaxHp; i++)
        {
            if (i <= Hp - 1)
            {
                Hpbars[i].SetActive(true);
            }
            else
            {
                Hpbars[i].SetActive(false);
            }
        }
        if (Hp <= 0)
            SceneManager.LoadScene("Main");
    }

    void Camera_move()
    {
        if (cant_cam)
            return;

        Vector3 pos = Vector3.Lerp(Camera.transform.position, transform.position + (Vector3.up * 2), camera_speed * Time.deltaTime);
        Camera.transform.position = new Vector3(pos.x, pos.y, -10f);
    }

    IEnumerator HitAnimation(Enemy enemy)
    {
        Invincibility = true;
        Hp -= enemy.Attack;
        float T = 0;
        float Alpha = 1;
        Knockback = true;
        Vector2 Dir = enemy.transform.position - transform.position;
        int Direction = Math.Sign(Dir.x);
        RG.velocity = new Vector2(-2.5f * Direction, RG.velocity.y);
        while (T < InvincibilityTime)
        {
            yield return new WaitForSeconds(0.1f);
            T += 0.1f;
            if (T == 0.3f)
                Knockback = false;
            if (Alpha > 0.5f)
            {
                Alpha -= 0.1f;
            }
            else
            {
                Alpha += 0.1f;
            }
            SR.color = new Color(1, 1, 1, Alpha);
        }
        SR.color = Color.white;
        Invincibility = false;
    }
}