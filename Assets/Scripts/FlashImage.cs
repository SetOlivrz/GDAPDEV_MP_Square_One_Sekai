using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class FlashImage : MonoBehaviour
{
    Image image = null;
    Coroutine currFlashRoutine = null;


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartFlash(float seconds, float maxAlpha, Color newColor)
    {
        image.color = newColor;

        // clamp between 0-1
        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1); 

        if (currFlashRoutine != null)
        {
            StopCoroutine(currFlashRoutine);
        }

        currFlashRoutine = StartCoroutine(Flash(seconds, maxAlpha));

    }

    IEnumerator Flash(float seconds, float maxAlpha)
    {
        float flashInDuration = seconds / 2;

        for (float i = 0; i <= flashInDuration; i += Time.deltaTime)
        {
            Color tempColor = image.color;
            tempColor.a = Mathf.Lerp(0, maxAlpha, i/ flashInDuration);
            image.color = tempColor;

            yield return null;

        }

        float flashOutDuration = seconds / 2;
        for (float t = 0; t < flashInDuration; t += Time.deltaTime) 
        {
            Color tempColor = image.color;
            tempColor.a = Mathf.Lerp(maxAlpha, 0, t / flashInDuration);
            image.color = tempColor;
            yield return null;
        }

        image.color = new Color32(0, 0, 0, 0);
    }
}
