using UnityEditor;
using UnityEngine;

public class CopyAssetPathContextMenu
{
    [MenuItem("Assets/Copy Asset Path")]
    public static void CopyAssetPath()
    {
        if (Selection.activeObject != null)
        {
            var assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            EditorGUIUtility.systemCopyBuffer = assetPath;
            Debug.Log("Copied to Buffer:" + assetPath);
        }
        else
        {
            Debug.Log("Nothing selected.");
        }
    }
}