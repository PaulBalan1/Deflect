using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    // Functionality for fade-in and fade-out between scenes.

    [SerializeField] private Image overlay;

    public static event Action OnFadeInFinished;
    public static event Action OnFadeOutFinished;

    public void SetColor(Color color)
    {
        overlay.color = color;
    }

    public void FadeOut(float duration = 0.5f)
    {
        StartCoroutine(FadeOutRoutine(duration));
    }
    
    public void FadeIn(Color color, float duration = 0.5f)
    {
        StartCoroutine(FadeInRoutine(color, duration));
    }

    private IEnumerator FadeOutRoutine(float duration)
    {
        float time = 0;
        Color startValue = overlay.color;
        while (time < duration)
        {
            overlay.color = Color.Lerp(startValue, Color.clear, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        overlay.color = Color.clear;
        OnFadeOutFinished?.Invoke();
    }


    private IEnumerator FadeInRoutine(Color endColor, float duration)
    {
        float time = 0;
        Color startValue = overlay.color;
        while (time < duration)
        {
            overlay.color = Color.Lerp(startValue, endColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        overlay.color = endColor;
        OnFadeInFinished?.Invoke();
    }
}
