using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour, IInteractable
{
    public string type;
    public string desc = "Pick";
    public bool dead = false;

    public void OnActivate()
    {
        if(transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }

    public string GetDesc()
    {
        if (dead)
        {
            return desc;
        }
        return "";
    }

    public string GetEnemy()
    {
        return type;
    }
}
