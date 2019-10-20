using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvasScaleAdjuster : MonoBehaviour
{
    RectTransform rootRect;

    // Start is called before the first frame update
    void Awake()
    {
        rootRect = gameObject.GetComponent<RectTransform>();
        AdjustScale();
    }

    void AdjustScale()
    {
        // Unity game view scaling only fits height and cuts width
        var unityDefaultScale = Screen.height / rootRect.rect.height;
        var unityScaledWidth = rootRect.rect.width * unityDefaultScale;

        // if canvas width already fits in screen, do nothing
        if (unityScaledWidth > Screen.width)
        {
            var adjustedScale = Screen.width / unityScaledWidth;
            transform.localScale = new Vector3(adjustedScale, adjustedScale);
        }
    }
}
