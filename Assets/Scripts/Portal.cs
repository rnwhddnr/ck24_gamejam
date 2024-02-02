using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] Vector2 Target = new Vector2();
    [SerializeField] bool Move_Game_or_Shop;

    private void Start()
    {
        if (Move_Game_or_Shop)
            GetComponent<Interaction>().interact += move_shop_or_Game;
        else
            GetComponent<Interaction>().interact += MovePosition;
    }
    void MovePosition()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(5, 5), 0, Vector3.forward, 1, 1 << 6);
        if (hit.collider == null)
            return;
        if(hit.transform.CompareTag("Player"))
        {
            hit.transform.position = Target;
        }
    }
    private void move_shop_or_Game()
    {
        if (SceneManager.GetActiveScene().name == "Game")
            SceneManager.LoadScene("Shop");
        else
            SceneManager.LoadScene("Game");
    }
}
