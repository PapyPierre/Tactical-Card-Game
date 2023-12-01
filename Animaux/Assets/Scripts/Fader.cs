using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] Image      image;
    [SerializeField] float      fadingTime = 0.5f;
    [SerializeField] FadingType fadingType;

    float elapsedTime;
    float a;

    [ContextMenu("Fade")]
    public void Fade()
    {
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        elapsedTime = 0;

        while (elapsedTime < fadingTime)
        {
            elapsedTime += Time.unscaledDeltaTime;

            switch (fadingType)
            {
                case FadingType.FadeIn:
                    a = (fadingTime - (fadingTime - elapsedTime)) / fadingTime;
                    break;
                case FadingType.FadeOut:
                    a = (fadingTime - elapsedTime) / fadingTime;
                    break;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return null;
        }

        switch (fadingType)
        {
            case FadingType.FadeIn:
                a = 1;
                break;
            case FadingType.FadeOut:
                a = 0;
                break;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, a);
    }
}

public enum FadingType
{
    FadeOut,
    FadeIn,
}