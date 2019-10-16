using UnityEngine;
using UnityEngine.UI;

public class FillingBar : MonoBehaviour
{
    internal bool decreasing;

    Image backFiller;
    Slider bar;

    float animationSpeed = 0.01f;
    float targetValue;

    void Awake()
    {
        backFiller = transform.GetChild(0).GetComponent<Image>();
        bar = GetComponent<Slider>();
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
                // change front filler immediately, animate back filler decrease
                bar.value = targetValue;
            }
            else
            {
                decreasing = false;
                // change back filler immediately, animate front filler increase
                backFiller.fillAmount = newValue;
            }
        }
    }

    void LateUpdate()
    {
        if (decreasing)
        {
            if (Mathf.Abs(targetValue - backFiller.fillAmount) > animationSpeed)
                backFiller.fillAmount -= animationSpeed;
            else
                backFiller.fillAmount = targetValue;
        }
        else
        {
            if (Mathf.Abs(targetValue - bar.value) > animationSpeed)
                bar.value += animationSpeed;
            else
                bar.value = targetValue;
        }
    }
}
