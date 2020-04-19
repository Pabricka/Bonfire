using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator anim;

    public Monologue monologue;

    public Kokko kokko;

    public GameObject spider;
    public GameObject demon;
    public GameObject bat;
    public GameObject torch;

    public bool carrying;

    public Text text;

    public Camera fpsCam;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    float lastDmg = 0f;

    public CharacterController controller;

    public Vector3 move;

    Vector3 velocity;
    bool isGrounded;
    public bool dead;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
        if (dead){ return; }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(kokko.fuel <= 0f)
        {
            GameOver();
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            StartCoroutine(fpsCam.GetComponent<CameraShake>().Shake(3f, 25f));
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, 4f))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                text.text = interactable.GetDesc();

                if (Input.GetButtonDown("Fire2"))
                {
                    if(interactable.GetEnemy() == "spider" && kokko.level > 0 && !carrying)
                    {
                        interactable.OnActivate();
                        carrying = true;
                        spider.SetActive(true);
                        torch.SetActive(false);
                        kokko.desc = "Feed";
                    }
                    else if (interactable.GetEnemy() == "demon" && kokko.level > 0 && !carrying)
                    {
                        interactable.OnActivate();
                        carrying = true;
                        demon.SetActive(true);
                        torch.SetActive(false);
                        kokko.desc = "Feed";
                    }
                    else if (interactable.GetEnemy() == "bat" && kokko.level > 0 && !carrying)
                    {
                        interactable.OnActivate();
                        carrying = true;
                        bat.SetActive(true);
                        torch.SetActive(false);
                        kokko.desc = "Feed";
                    }
                    else
                    if(interactable.GetEnemy() == "kokko")
                    {
                        if(kokko.level == 0)
                        {
                            monologue.ReadNext();
                        }

                        if (kokko.level == 7)
                        {
                            Ending();
                            kokko.level = 100;
                            kokko.fuel = 2f;
                        }

                        interactable.OnActivate();
                        if (carrying)
                        {

                            monologue.ReadNext();
                            carrying = false;
                            kokko.Stoke();
                            spider.SetActive(false);
                            demon.SetActive(false);
                            bat.SetActive(false);
                            torch.SetActive(true);
                            kokko.desc = "";
                            if (kokko.level == 5)
                            {
                                GroundShake();
                            }
                            if (kokko.level == 6)
                            {
                                kokko.desc = "Embrace";
                                kokko.level += 1;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            text.text = "";
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void TakeDamage(float dmg)
    {
        if(Time.time > lastDmg + 2f)
        {
            kokko.fuel -= dmg;
            anim.SetTrigger("damage");
            lastDmg = Time.time;
        }

    }

    public void GameOver()
    {
        anim.enabled = true;
        fpsCam.GetComponent<MouseLook>().dead = true;
        dead = true;
        anim.SetTrigger("death");
    }

    public void GroundShake()
    {
        anim.enabled = false;
        StartCoroutine(fpsCam.GetComponent<CameraShake>().Shake(3f, 0.25f));
    }

    public void Ending()
    {

        anim.enabled = true;
        dead = true;
        fpsCam.transform.DetachChildren();
        anim.SetTrigger("ending");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
