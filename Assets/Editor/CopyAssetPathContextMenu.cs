using UnityEditor;

public class CopyAssetPathContextMenu
{
    [MenuItem("Assets/Copy Asset Path")]
    public static void CopyAssetPath()
    {
        if (Selection.activeObject != null)
            EditorGUIUtility.systemCopyBuffer = AssetDatabase.GetAssetPath(Selection.activeObject)
                .Replace(@"Assets/Resources/", "")
                .Replace(".asset", "");
    }
}