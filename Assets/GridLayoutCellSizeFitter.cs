using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutCellSizeFitter : MonoBehaviour
{
    public int maxElementsUntilResize;
    public float minScale = 0.5f;

    GridLayoutGroup gridLayout;
    float currentScale = 1;

    // Use this for initialization
    void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
    }

    internal void AddCell(GameObject obj)
    {
        obj.transform.SetAsLastSibling();
        RecalculateCellSize();
    }

    public void RecalculateCellSize()
    {
        if (gridLayout.transform.childCount > maxElementsUntilResize && currentScale >= minScale / 2)
        {
            //curMaxElemUntilRes *= 2;
            currentScale /= 2;
            gridLayout.cellSize /= 2;
        }

        //else
        //{
        //    curMaxElemUntilRes /= 2;
        //    currentScale *= 2;
        //    gridLayout.cellSize *= 2;
        //}
    }
}