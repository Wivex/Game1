using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HiddenIfNotAttribute))]
public class HiddenIfNotPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var wasEnabled = GUI.enabled;
        GUI.enabled = IsAllowed((HiddenIfNotAttribute) attribute, property);
        if (GUI.enabled)
            EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return IsAllowed((HiddenIfNotAttribute) attribute, property)
            ? EditorGUI.GetPropertyHeight(property, label)
            : -EditorGUIUtility.standardVerticalSpacing;
    }

    protected bool IsAllowed(HiddenIfNotAttribute attribute, SerializedProperty property)
    {
        //Get the full relative property path of the sourcefield so we can have nested hiding
        //returns the property path of the property we want to apply the attribute to
        var propertyPath = property.propertyPath;
        //changes the path to the conditionalsource property path
        var conditionPath = propertyPath.Replace(property.name, attribute.condProperty);
        var sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        return IsSupportedPropertyType(sourcePropertyValue);
    }

    protected bool IsSupportedPropertyType(SerializedProperty sourcePropertyValue)
    {
        switch (sourcePropertyValue?.propertyType)
        {
            case SerializedPropertyType.Boolean:
                return sourcePropertyValue.boolValue;
            case SerializedPropertyType.ObjectReference:
                return sourcePropertyValue.objectReferenceValue != null;
            default:
                Debug.LogError("Data type of the property used for conditional hiding [" +
                               sourcePropertyValue.propertyType + "] is currently not supported");
                return true;
        }
    }
}

[CustomPropertyDrawer(typeof(DisabledIfNotAttribute))]
public class DisabledIfNotPropertyDrawer : HiddenIfNotPropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var wasEnabled = GUI.enabled;
        GUI.enabled = IsAllowed((DisabledIfNotAttribute) attribute, property);
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label);
    }
}