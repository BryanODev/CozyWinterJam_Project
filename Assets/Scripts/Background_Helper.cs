using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background_Helper : MonoBehaviour
{
    bool FadeOut = false;
    Color backgroundColor;
    public float opacity = 1;
    Image spriteRenderer;

    public float opacityDifference = 0.1f;
    public float opacityMin;
    public float opacityMax;

    // Start is called before the first frame update
    void Start()
    {
       spriteRenderer = GetComponent<Image>();
        backgroundColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (FadeOut)
        {
            opacity -= opacityDifference * Time.deltaTime;

            if (opacity <= opacityMin)
            {
                FadeOut = false;
            }
        }
        else 
        {
            opacity += opacityDifference * Time.deltaTime;

            if (opacity >= opacityMax) 
            {
                FadeOut = true;
            }
        }

        backgroundColor.a = opacity;
        spriteRenderer.color = backgroundColor;
    }
}
