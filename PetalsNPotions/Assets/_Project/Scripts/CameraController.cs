using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private RawImage fadeImage;
    private bool shouldfadeToBlack = false;
    private bool setblackfadeOff = false;

    private void Awake()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
    }

    public void FadeToBlack()
    {
        shouldfadeToBlack = true;
        setblackfadeOff = false;
         
    }

    public void RevertBlackFade()
    {
        shouldfadeToBlack = false;
        setblackfadeOff = true;
    }

    private void Update()
    {
        if(shouldfadeToBlack)
        {
            float alpha = Mathf.Lerp(fadeImage.color.a, 1, Time.deltaTime * 10);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);

            if(alpha >= 1)
            {
                shouldfadeToBlack = false;
            }
        }

        if (setblackfadeOff)
        {
            float alpha = Mathf.Lerp(fadeImage.color.a, 0, Time.deltaTime * 10);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);

            if (alpha <= 0)
            {
                setblackfadeOff = false;
            }
        }
    }
}
