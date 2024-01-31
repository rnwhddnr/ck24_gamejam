using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public delegate void Interact();
    public Interact interact;
    public void InteractStart()
    {
        if (interact != null)
        {
            interact();
        }
    }
}
