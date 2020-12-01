using System;
using UnityEngine;
using UnityEngine.UI;

public enum StatBarState
{
    Neutral,
    Increasing,
    Decreasing
}

public class StatBar : MonoBehaviour
{

    public Color decreasingBackFillerColor, increasingBackFillerColor;

    StatBarState state = StatBarState.Neutral;
    Image backFiller;
    Slider bar;
    float targetPercent;

    float animationSpeed = 0.005f;

    void Awake()
    {
        backFiller = transform.GetChild(0).GetComponent<Image>();
        bar = GetComponent<Slider>();
    }

    internal void SetInstantValue(float percent)
    {
        targetPercent = percent;
        bar.value = targetPercent;
        backFiller.fillAmount = percent;
    }

    internal void SetTargetShiftingValue(float percent, float animationSpeed = 0.005f)
    {
        targetPercent = percent;
        this.animationSpeed = animationSpeed;
        if (targetPercent < bar.value)
        {
            state = StatBarState.Decreasing;
            backFiller.color = decreasingBackFillerColor;
            // change front filler immediately, animate back filler decrease
            bar.value = targetPercent;
        }
        else
        {
            state = StatBarState.Increasing;
            backFiller.color = increasingBackFillerColor;
            // change back filler immediately, animate front filler increase
            backFiller.fillAmount = percent;
        }
    }

    void LateUpdate()
    {
        switch (state)
        {
            case StatBarState.Increasing:
                // TimeScale adjustment so that animation synced with the current game speed
                if (Mathf.Abs(targetPercent - bar.value) > animationSpeed * Time.timeScale)
                    bar.value += animationSpeed * Time.timeScale;
                else
                {
                    // targetPercent reached
                    bar.value = targetPercent;
                    state = StatBarState.Neutral;
                }
                break;
            case StatBarState.Decreasing:
                if (Mathf.Abs(targetPercent - backFiller.fillAmount) > animationSpeed * Time.timeScale)
                    backFiller.fillAmount -= animationSpeed * Time.timeScale;
                else
                {
                    backFiller.fillAmount = targetPercent;
                    state = StatBarState.Neutral;
                }
                break;
        }
    }
}
