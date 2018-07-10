using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class HiddenIfNotAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string condProperty;

    public HiddenIfNotAttribute(string condProperty)
    {
        this.condProperty = condProperty;
    }
   
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class DisabledIfNotAttribute : HiddenIfNotAttribute
{
    public DisabledIfNotAttribute(string condProperty) : base(condProperty) { }

}