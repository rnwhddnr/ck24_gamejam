using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Camera Camera;
    Canvas canvas;
    GameManager GM;
    Rigidbody2D RG;
    bool Invincibility;
    [SerializeField] SpriteRenderer SR;
    public float InvincibilityTime;
    public int JumpCount = 1;
    public int MaxJumpCount = 1;
    bool NowJump;
    public int Speed;
    public int MaxHp;
    int hp;
    public float camera_speed;

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
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        GM = GameManager.instance;
        RG = GetComponent<Rigidbody2D>();
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
    void Update()
    {
        Move();
        Interact();
    }
    private void LateUpdate()
    {
        Camera_move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Block") && NowJump)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.52f, 1 << 3);
            Debug.DrawRay(transform.position, Vector2.down * 0.52f, Color.red);
            if (hit.collider != null && hit.transform.CompareTag("Block"))
            {
                JumpCount = MaxJumpCount;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (Invincibility)
                return;
            StartCoroutine(HitAnimation());
        }
    }
    void Move()
    {
        RG.velocity = new Vector2(0, RG.velocity.y);
        int XMove = 0;
        if (Input.GetKey(GM.OperationKey["RightMove"]))
        {
            XMove += 1 * Speed;
        }
        else if (Input.GetKey(GM.OperationKey["LeftMove"]))
        {
            XMove -= 1 * Speed;
        }
        RG.velocity = new Vector2(XMove, RG.velocity.y);
        if (Input.GetKeyDown(GM.OperationKey["Jump"]))
        {
            if (JumpCount > 0)
            {
                RG.velocity += new Vector2(0, 5);
                JumpCount--;
                NowJump = true;
            }
        }
    }
    void Interact()
    {
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
        if (Hp == 0)
            SceneManager.LoadScene("Main");
    }

    void Camera_move()
    {
        Vector3 pos = Vector3.Lerp(Camera.transform.position, transform.position, camera_speed);
        Camera.transform.position = pos;
    }

    IEnumerator HitAnimation()
    {
        Invincibility = true;
        Hp -= 1;
        float T = 0;
        float Alpha = 1;
        while (T < InvincibilityTime)
        {
            yield return new WaitForSeconds(0.1f);
            T += 0.1f;
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
