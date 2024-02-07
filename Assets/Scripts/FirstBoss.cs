using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : MonoBehaviour
{
    Enemy Enemy;
    Animator animator;
    [SerializeField] string[] OnePattern = { "LeftHandAttack", "RightHandAttack" };
    [SerializeField] string[] TwoPattern = { "DownClap", "UpClap" };
    [SerializeField] string[] HeadPattern = { "HeadBanging" };
    WaitForSeconds WaitForSeconds=new WaitForSeconds(3.5f);
    [SerializeField] GameObject[] Monsters = new GameObject[5];
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }
    IEnumerator Start()
    {
        while (true)
        {
            yield return null;
            if (animator.GetBool("End"))
                break;
        }
        while(true)
        {
            yield return WaitForSeconds;
            int i=Random.Range(0, 10);
            if (i >= 8)
                Head();
            else if (i >= 5)
                Two();
            else
                One();
        }
    }
    void One()
    {
        int i=Random.Range(0, OnePattern.Length);
        TriggerOn(OnePattern[i]);
        animator.SetBool("Idle", true);
    }
    void Two()
    {
        int i = Random.Range(0, 100);
        if(i<45)
        {
            TriggerOn(TwoPattern[1]);
        }
        else
            TriggerOn(TwoPattern[0]);
        animator.SetBool("Idle", true);
    }
    void Head()
    {
        TriggerOn(HeadPattern[0]);
        animator.SetBool("Idle", true);
    }
    void TriggerOn(string TriggerName)
    {
        animator.SetTrigger(TriggerName);
    }
    public void BOOL()
    {
        animator.SetBool("GroundAttack", true);
    }
    public void End()
    {
        animator.SetBool("End", true);
    }
    public void AttackEnd()
    {
        animator.SetBool("Idle", true);
    }
    public void AttackStart()
    {
        animator.SetBool("Idle", false);
    }
    public void Mobspawn()
    {
        for(int i = 0; i < 5; i++)
        {
            int a=Random.Range(0, 10);
            GameObject OBJ = null;
            if (a >= 9)
                OBJ = Instantiate(Monsters[0], Enemy.Center.transform.position, Quaternion.identity);

            else if (a >= 7)
                OBJ = Instantiate(Monsters[1], Enemy.Center.transform.position, Quaternion.identity);

            else if(a >= 5)
                OBJ = Instantiate(Monsters[2], Enemy.Center.transform.position, Quaternion.identity);

            else if (a >= 3)
                OBJ = Instantiate(Monsters[3], Enemy.Center.transform.position, Quaternion.identity);

            else
                OBJ = Instantiate(Monsters[4], Enemy.Center.transform.position, Quaternion.identity);

            if (OBJ != null)
                OBJ.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1, 1) * 7, 0);
        }
    }
}
