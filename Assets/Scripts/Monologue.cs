using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Monologue : MonoBehaviour
{
    public AudioSource audioS;
    public Text text;
    public string[] lines;

    int counter = -1;

    public void Start()
    {
        ReadNext();
    }

    public void ReadNext()
    {
        counter += 1;
        text.text = "";
        StopAllCoroutines();
        StartCoroutine(FillText());

    }

    IEnumerator FillText()
    {
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < lines[counter].Length; i++)
        {
            text.text += lines[counter][i];
            audioS.pitch = counter * -0.1f + Random.Range(0.5f, 1.5f);
            audioS.Play();
            yield return new WaitForSeconds(0.05f);
            

        }
        audioS.Stop();
        yield return new WaitForSeconds(2f);
        text.text = "";
    }
}
