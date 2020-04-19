using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Kokko : MonoBehaviour, IInteractable
{
    public int level = 0;
    public Light l;
    public Text text;
    public Slider slider;
    public string desc;
    public ParticleSystem ps;
    public float fuel = 0.01f;
    public float decrease = 0.01f;
    bool active = false;
       
    void Update()
    {
        if (active)
        {
            fuel -= decrease * Time.deltaTime;
            RenderSettings.fogDensity = 0.17f - (fuel / 10);
            slider.value = fuel;
        }

    }

    public string GetDesc()
    {
        return desc;
    }

    public string GetEnemy()
    {
        return "kokko";
    }

    public void OnActivate()
    {
        if (!active)
        {
            fuel = 1f;
            active = true;
            l.enabled = true;
            ps.Play();
            desc = "";
            level += 1;
        }

    }

    public void Stoke()
    {
        level += 1;
        fuel += 0.45f;
        if(fuel > 1f)
        {
            fuel = 1f;
        }
    }

}
