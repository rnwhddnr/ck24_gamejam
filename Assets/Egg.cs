using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Egg : MonoBehaviour
{
    Enemy Enemy;
    [SerializeField] Sprite DefaultSprite;
    [SerializeField] Sprite MoveSprite;
    bool Ani;
    WaitForSeconds WaitForSeconds = new WaitForSeconds(0.2f);

    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        Enemy.Go = MoveStart;
    }
    void MoveStart()
    {
        if (!Ani)
            StartCoroutine(MoveAnimation());
    }
    IEnumerator MoveAnimation()
    {
        Ani = true;
        Enemy.SR.sprite = MoveSprite;
        yield return WaitForSeconds;
        Enemy.SR.sprite = DefaultSprite;
        Ani = false;
    }
}
