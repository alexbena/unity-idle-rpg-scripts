using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact() 
    {
        Debug.Log("Interacting: " + transform.name);

    }

    public virtual void GetHit(int damage) 
    {
        Debug.Log("Hitting: " + transform.name);
    }
}
