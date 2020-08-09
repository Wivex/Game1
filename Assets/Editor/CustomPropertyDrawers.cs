using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HideIfNotBoolAttribute))]
public class HideIfNotBoolPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (IsAllowed((HideIfNotBoolAttribute)attribute, property))
            EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => IsAllowed((HideIfNotBoolAttribute) attribute, property)
        ? EditorGUI.GetPropertyHeight(property, label)
        : -EditorGUIUtility.standardVerticalSpacing;

    protected bool IsAllowed(HideIfNotBoolAttribute attr, SerializedProperty property)
    {
        //Get the full relative property path of the sourcefield so we can have nested hiding
        //returns the property path of the property we want to apply the attribute to
        var propertyPath = property.propertyPath;
        //changes the path to the conditional source property path
        var conditionPath = propertyPath.Replace(property.name, attr.boolPropertyName);
        var sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        return IsSupportedPropertyType(sourcePropertyValue);
    }

    protected bool IsSupportedPropertyType(SerializedProperty sourcePropertyValue)
    {
        switch (sourcePropertyValue?.propertyType)
        {
            case SerializedPropertyType.Boolean:
                return sourcePropertyValue.boolValue;
            default:
                Debug.LogError("Data type of the property used for conditional hiding [" +
                               sourcePropertyValue.propertyType + "] is currently not supported");
                return false;
        }
    }
}

/// <summary>
/// cannot hide [Range] and other attribute-generated properties
/// </summary>
[CustomPropertyDrawer(typeof(HideIfNotEnumValuesAttribute))]
public class HideIfNotEnumValuesPropertyDrawer : PropertyDrawer
{
    float propertyHeight;

    // removes empty space instead of "not drawn" property
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        propertyHeight = IsAllowed((HideIfNotEnumValuesAttribute)attribute, property)
            ? EditorGUI.GetPropertyHeight(property, label)
            : -EditorGUIUtility.standardVerticalSpacing;
        return propertyHeight;
    }
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (propertyHeight > 0)
            EditorGUI.PropertyField(position, property, label, true);
    }

    protected bool IsAllowed(HideIfNotEnumValuesAttribute attr, SerializedProperty property)
    {
        var propertyPath = property.propertyPath;
        var enumPath = propertyPath.Replace(property.name, attr.enumPropertyName);
        // find enum value through our property object
        var enumProperty = property.serializedObject.FindProperty(enumPath);
        var enumIndex = enumProperty?.enumValueIndex;

        return IsSupportedPropertyType(enumProperty) && attr.enumValues.Contains((int)enumIndex);
    }

    protected bool IsSupportedPropertyType(SerializedProperty sourcePropertyValue)
    {
        switch (sourcePropertyValue?.propertyType)
        {
            case SerializedPropertyType.Enum:
                return true;
            default:
                //Debug.LogError("Data type of the property used for conditional hiding [" + sourcePropertyValue.propertyType + "] is currently not supported");
                //return false;
                return true;
        }
    }
}

[CustomPropertyDrawer(typeof(DisabledAttribute))]
public class DisabledDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
        EditorGUI.GetPropertyHeight(property, label, true);
}

[CustomPropertyDrawer(typeof(DisabledIfNotBoolAttribute))]
public class DisabledIfNotBoolPropertyDrawer : HideIfNotBoolPropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = IsAllowed((DisabledIfNotBoolAttribute) attribute, property);
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label);
}

[CustomPropertyDrawer(typeof(StringInListAttribute))]
public class StringInListDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attrs = fieldInfo.GetCustomAttributes(typeof(PropertyAttribute), false);
        var attr = attribute as StringInListAttribute;
        var names = new List<string>();
        var listProp = property.serializedObject.FindProperty(attr.listName);
        for (var i = 0; i < listProp.arraySize; i++) names.Add(listProp.GetArrayElementAtIndex(i).stringValue);
        if (names.NotNullOrEmpty())
        {
            var selInd = Mathf.Max(names.IndexOf(property.stringValue), 0);
            selInd = EditorGUI.Popup(position, property.name, selInd, names.ToArray());
            property.stringValue = names[selInd];
        }
        else
            EditorGUI.PropertyField(position, property, label);
    }
}