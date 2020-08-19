using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutHandler : MonoBehaviour
{
    public float minScale = 0.5f, maxScale = 1f;

    GridLayoutGroup gridLayout;
    Rect rect;
    float curScale = 1;

    Dictionary<object, GameObject> gridContent = new Dictionary<object, GameObject>();

    // Use this for initialization
    void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rect = (transform as RectTransform).rect;
    }

    bool NeedCellsShrink =>
        gridLayout.cellSize.x * gridLayout.transform.childCount > rect.width ||
        gridLayout.cellSize.y * gridLayout.transform.childCount > rect.height;

    bool NeedCellsEnlarge =>
        gridLayout.cellSize.x * 2 * gridLayout.transform.childCount <= rect.width &&
        gridLayout.cellSize.y * 2 * gridLayout.transform.childCount <= rect.height;

    internal void AddCell(object obj, GameObject gameObj)
    {
        gridContent.Add(obj, gameObj);
        gameObj.transform.SetParent(transform);
        RecalculateCellSize();
    }

    internal void RemoveCell(object obj)
    {
        Destroy(gridContent[obj]);
        gridContent.Remove(obj);
        RecalculateCellSize();
    }

    void RecalculateCellSize()
    {
        if (NeedCellsShrink && curScale / 2 >= minScale)
        {
            curScale /= 2;
            gridLayout.cellSize /= 2;
        }

        if (NeedCellsEnlarge && curScale * 2 <= maxScale)
        {
            curScale *= 2;
            gridLayout.cellSize *= 2;
        }
    }
}