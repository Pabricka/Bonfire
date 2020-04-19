using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if(health<= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        transform.gameObject.tag = "Firewood";
        Spider s = GetComponent<Spider>();
        if(s != null)
        {
            s.Die();
            return;
        }
        Demon d = GetComponent<Demon>();
        if (d != null)
        {
            d.Die();
            return;
        }
        GetComponent<Animator>().SetTrigger("death");
        GetComponentInChildren<Corpse>().dead = true;
    }
}
