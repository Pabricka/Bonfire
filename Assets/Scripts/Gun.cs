
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject blood;
    public float damage = 10f;
    public int ammo = 2;

    public float range = 100f;

    public Camera fpsCam;

    public bool reloading;
    public bool shooting;
    Animator animator;

    ParticleSystem ps;

    AudioSource audioS;
    public AudioSource reloadS;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && reloading == false)
        {
            if (!shooting && ammo > 0)
            {
                shooting = true;
                Shoot();
                ammo -= 1;
                ps.Play();

                animator.SetTrigger("shoot");
                audioS.Play();
            }

            if(ammo <= 0)
            {
                animator.SetTrigger("reload");
                StartCoroutine(ReloadSound());
            }
        }
    }

    void Shoot()
    {

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Debug.Log(enemy);
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
                Instantiate(blood, hit.point, enemy.transform.rotation);
            }
            enemy = hit.transform.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Instantiate(blood, hit.point, enemy.transform.rotation);
            }
        }
    }
    void RefillAmmo()
    {
        ammo = 2;
    }

    public IEnumerator ReloadSound()
    {
        yield return new WaitForSeconds(0.6f);
        reloadS.Play();
    }


}
