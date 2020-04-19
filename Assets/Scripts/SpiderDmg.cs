using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDmg : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement p = other.GetComponent<PlayerMovement>();
        if (p != null)
        {
            p.TakeDamage(0.10f);
        }
    }
}
