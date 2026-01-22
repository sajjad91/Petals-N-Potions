using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    [SerializeField] private Color fadeColor;
    [SerializeField] private Color NormalColor;

    public void FadeColor(SpriteRenderer[] sprites)
    {
        for(int i = 0; i< sprites.Length; i++)
        {
            sprites[i].color = Color.Lerp(sprites[i].color, fadeColor, Time.deltaTime * 2);
        }
    }

    public void UnfadeColor(SpriteRenderer[] spirites)
    {
        for (int i = 0; i < spirites.Length; i++)
        {
            spirites[i].color = Color.Lerp(spirites[i].color, NormalColor, Time.deltaTime * 2);
        }
    }
}
