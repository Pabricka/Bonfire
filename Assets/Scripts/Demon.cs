using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{

    public GameObject player;
    public bool active = false;
    public bool dead = false;

    public bool canShoot =  true;
    public GameObject fireball;

    public float speed = 1f;

    public Animator animator;
    Corpse corpse;
    float stray;
    void Start()
    {
        animator = GetComponent<Animator>();
        corpse = GetComponent<Corpse>();
    }

    void Update()
    {
        if (active && !dead)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > 15)
            {
                transform.Translate(stray * speed * Time.deltaTime, 0, speed * Time.deltaTime);
            }

            if (canShoot)
            {
                canShoot = false;
                GameObject fb = Instantiate(fireball, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation);
                StartCoroutine(Cooldown());
            }
        }
    }

    public void Die()
    {
        if (!dead)
        {

            animator.SetTrigger("death");
            active = false;
            dead = true;
        }
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        canShoot = true;
    }

    public IEnumerator ChangeDirection()
    {
        while (true)
        {
            stray = Random.Range(-1f, 1f);
            yield return new WaitForSeconds(1);
        }

    }
}
