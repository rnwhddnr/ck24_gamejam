using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Vector2 Target = new Vector2();
    private void Start()
    {
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
}
