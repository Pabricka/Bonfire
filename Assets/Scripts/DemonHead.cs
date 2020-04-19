using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHead : MonoBehaviour
{

    public Demon demon;

    private void OnTriggerEnter(Collider other)
    {
        if (!demon.dead)
        {
            if (other.CompareTag("Player"))
            {
                demon.player = other.gameObject;
                demon.active = true;
                demon.canShoot = true;
                demon.animator.SetBool("Active", true);
                Destroy(this.gameObject.GetComponent<BoxCollider>());
                Destroy(this);
            }
        }
    }
}
