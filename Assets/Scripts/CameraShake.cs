using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magniteude)
    {
        Vector3 original = transform.localPosition;
        GetComponent<AudioSource>().Play();
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magniteude;
            float y = Random.Range(-1f, 1f) * magniteude;

            elapsed += Time.deltaTime;

            transform.localPosition = new Vector3(x, y, original.z);
            yield return null;
        }

        transform.localPosition = original;
    }

}
