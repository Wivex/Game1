﻿using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HiddenIfNotAttribute))]
public class HiddenIfNotPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (IsAllowed((HiddenIfNotAttribute)attribute, property))
            EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => IsAllowed((HiddenIfNotAttribute) attribute, property)
        ? EditorGUI.GetPropertyHeight(property, label)
        : -EditorGUIUtility.standardVerticalSpacing;

    protected bool IsAllowed(HiddenIfNotAttribute attr, SerializedProperty property)
    {
        //Get the full relative property path of the sourcefield so we can have nested hiding
        //returns the property path of the property we want to apply the attribute to
        var propertyPath = property.propertyPath;
        //changes the path to the conditionalsource property path
        var conditionPath = propertyPath.Replace(property.name, attr.condPropertyName);
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


[CustomPropertyDrawer(typeof(DisabledIfNotAttribute))]
public class DisabledIfNotPropertyDrawer : HiddenIfNotPropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = IsAllowed((DisabledIfNotAttribute) attribute, property);
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label);
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

/// <summary>
/// cannot hide [Range] and other attribute-generated properties
/// </summary>
[CustomPropertyDrawer(typeof(ShownIfEnumValueAttribute))]
public class ShownIfEnumValuePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (IsAllowed((ShownIfEnumValueAttribute)attribute, property))
            EditorGUI.PropertyField(position, property, label, true);
    }

    // removes empty space instead of "not drawn" property
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
        IsAllowed((ShownIfEnumValueAttribute) attribute, property)
            ? EditorGUI.GetPropertyHeight(property, label)
            : -EditorGUIUtility.standardVerticalSpacing;

    protected bool IsAllowed(ShownIfEnumValueAttribute attr, SerializedProperty property)
    {
        var propertyPath = property.propertyPath;
        var enumPath = propertyPath.Replace(property.name, attr.enumPropertyName);
        // find enum value through our property object
        var enumProperty = property.serializedObject.FindProperty(enumPath);
        var enumIndex = enumProperty?.enumValueIndex;

        return IsSupportedPropertyType(enumProperty) && enumIndex == attr.enumValue;
    }

    protected bool IsSupportedPropertyType(SerializedProperty sourcePropertyValue)
    {
        switch (sourcePropertyValue?.propertyType)
        {
            case SerializedPropertyType.Enum:
                return true;
            default:
                Debug.LogError("Data type of the property used for conditional hiding [" +
                               sourcePropertyValue.propertyType + "] is currently not supported");
                return false;
        }
    }
}