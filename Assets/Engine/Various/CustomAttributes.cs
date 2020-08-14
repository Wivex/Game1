using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class HideIfNotBoolAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string boolPropertyName;

    public HideIfNotBoolAttribute(string boolPropertyName)
    {
        this.boolPropertyName = boolPropertyName;
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class HideIfNotEnumValuesAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string enumPropertyName;
    //The value of the enum field that will be in control
    public List<int> enumValues;

    public HideIfNotEnumValuesAttribute(string enumPropertyName, params object[] enums)
    {
        this.enumPropertyName = enumPropertyName;
        this.enumValues = enums.Cast<int>().ToList();
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class HideIfNotStringValuesAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string stringPropertyName;
    //The value of the enum field that will be in control
    public List<string> stringValues;

    public HideIfNotStringValuesAttribute(string stringPropertyName, params string[] stringValues)
    {
        this.stringPropertyName = stringPropertyName;
        this.stringValues = stringValues.ToList();
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class DisabledAttribute : PropertyAttribute { }

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class DisabledIfNotBoolAttribute : HideIfNotBoolAttribute
{
    public DisabledIfNotBoolAttribute(string boolPropertyName) : base(boolPropertyName) { }
}

public class StringInListAttribute : PropertyAttribute
{
    public string listName;

    public StringInListAttribute(string listName)
    {
        this.listName = listName;
    }
}