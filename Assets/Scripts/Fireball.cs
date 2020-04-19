using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 15f;
    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(Delete());
    }
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement p = other.GetComponent<PlayerMovement>();
        if (p != null)
        {
            p.TakeDamage(0.10f);
        }
        Destroy(gameObject);
    }
}
