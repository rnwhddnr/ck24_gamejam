using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    Player player;
    Enemy Enemy;
    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
}
