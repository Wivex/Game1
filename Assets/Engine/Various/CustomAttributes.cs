using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class HiddenIfNotAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string condPropertyName;

    public HiddenIfNotAttribute(string condPropertyName)
    {
        this.condPropertyName = condPropertyName;
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class DisabledIfNotAttribute : HiddenIfNotAttribute
{
    public DisabledIfNotAttribute(string condPropertyName) : base(condPropertyName) { }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class DisabledAttribute : PropertyAttribute { }

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class ShownIfEnumValueAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string enumPropertyName;
    //The value of the enum field that will be in control
    public int enumValue;

    public ShownIfEnumValueAttribute(string enumPropertyName, int enumValue)
    {
        this.enumPropertyName = enumPropertyName;
        this.enumValue = enumValue;
    }
}