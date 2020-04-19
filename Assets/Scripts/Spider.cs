using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    public GameObject player;
    public bool active = false;
    public bool dead = false;

    Corpse corpse;

    public float speed = 2f;

    Animator animator;
    void Start()
    {
        corpse = GetComponentInChildren<Corpse>();
        corpse.enabled = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (active && !dead)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if(direction.magnitude > 0.1f){
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            if (other.CompareTag("Player"))
            {
                player = other.gameObject;
                active = true;

                animator.SetBool("Active", true);
            }
        }
    }

    public void Die()
    {
        if (!dead)
        {
            GetComponent<AudioSource>().Play();
            animator.SetTrigger("death");
            active = false;
            dead = true;
            corpse.enabled = true;
        }
    }
}
