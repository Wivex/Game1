using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvasScaler : MonoBehaviour
{
    float rootCanvasWidth, rootCanvasHeight;
    RectTransform rootRect;

    // Start is called before the first frame update
    void Awake()
    {
        rootRect = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        SetScale();
    }

    void SetScale()
    {
        rootCanvasWidth = rootRect.rect.width;
        rootCanvasHeight = rootRect.rect.height;

        // Unity game view scaling only fits height and cuts width
        var unityDefaultScaleFactor = Screen.height / rootCanvasHeight;
        var unityScaledWidth = rootCanvasWidth * unityDefaultScaleFactor;
        var adjustedScaleFactor = Screen.width / unityScaledWidth;

        transform.localScale = new Vector3(adjustedScaleFactor, adjustedScaleFactor);
    }
}
