using UnityEngine;
using UnityEngine.UI;

public class FillingBar : MonoBehaviour
{
    public Color decreasingBackFillerColor, increasingBackFillerColor;

    internal bool decreasing;

    Image backFiller;
    Slider bar;

    float animationSpeed = 0.005f;
    float targetValue;

    void Awake()
    {
        backFiller = transform.GetChild(0).GetComponent<Image>();
        bar = GetComponent<Slider>();
    }

    /// <summary>
    /// Used to set current values directly to begin with
    /// </summary>
    internal void SetInitialValue(float newValue)
    {
        targetValue = newValue;
        bar.value = targetValue;
        backFiller.fillAmount = newValue;
    }

    internal void TryUpdateValue(float newValue)
    {
        // if new value differs from current one
        if (Mathf.Abs(targetValue - newValue) > Mathf.Epsilon)
        {
            targetValue = newValue;
            if (targetValue < bar.value)
            {
                decreasing = true;
                backFiller.color = decreasingBackFillerColor;

                // change front filler immediately, animate back filler decrease
                bar.value = targetValue;
            }
            else
            {
                decreasing = false;
                backFiller.color = increasingBackFillerColor;
                // change back filler immediately, animate front filler increase
                backFiller.fillAmount = newValue;
            }
        }
    }

    // TimeScale adjustment so that animation synced to current game speed
    void LateUpdate()
    {
        if (decreasing)
        {
            if (Mathf.Abs(targetValue - backFiller.fillAmount) > animationSpeed * Time.timeScale)
                backFiller.fillAmount -= animationSpeed * Time.timeScale;
            else
                backFiller.fillAmount = targetValue;
        }
        else
        {
            if (Mathf.Abs(targetValue - bar.value) > animationSpeed * Time.timeScale)
                bar.value += animationSpeed * Time.timeScale;
            else
                bar.value = targetValue;
        }
    }
}
