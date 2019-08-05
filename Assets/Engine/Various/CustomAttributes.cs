using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class HideIfNotAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string condPropertyName;

    public HideIfNotAttribute(string condPropertyName)
    {
        this.condPropertyName = condPropertyName;
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
public class DisabledAttribute : PropertyAttribute { }

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class DisabledIfNotAttribute : HideIfNotAttribute
{
    public DisabledIfNotAttribute(string condPropertyName) : base(condPropertyName) { }
}