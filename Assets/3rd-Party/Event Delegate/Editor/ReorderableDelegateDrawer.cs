﻿using System;
using UIEventDelegate;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomPropertyDrawer(typeof(ReorderableEventList), true)]
public class ReorderableDelegateDrawer : PropertyDrawer
{
    ReorderableList list;

    EventDelegate eventDelegate = new EventDelegate();

    GUIStyle headerBackground = "RL Header";

    [HideInInspector]
    public bool mShowList = true;

    /// <summary>
    /// The standard height size for each property line.
    /// </summary>

    const int lineHeight = 16;

    ReorderableList getList(SerializedProperty property)
    {
        if (list == null)
        {
            list = new ReorderableList(property.serializedObject, property, true, true, true, true);

            list.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                if (!mShowList)
                    return;

                rect.width -= 10;
                rect.x += 8;

                SerializedProperty elemtProp = property.GetArrayElementAtIndex(index);

                if (EditorApplication.isCompiling)
                {
                    SerializedProperty updateMethodsProp = elemtProp.FindPropertyRelative("mUpdateEntryList");
                    if(updateMethodsProp != null)
                        updateMethodsProp.boolValue = true;
                }

                EditorGUI.PropertyField(rect, elemtProp, true);
            };

            list.elementHeightCallback = index =>
            {
                if(!mShowList)
                    return 0;

                var element = property.GetArrayElementAtIndex(index);

                SerializedProperty showGroup = element.FindPropertyRelative("mShowGroup");
                if (!showGroup.boolValue)
                    return lineHeight + 1;

                float yOffset = 12;
                float lines = (3 * lineHeight) + yOffset;

                SerializedProperty targetProp = element.FindPropertyRelative("mTarget");
                if (targetProp.objectReferenceValue == null)
                    return lines;

                lines += lineHeight;

                SerializedProperty methodProp = element.FindPropertyRelative("mMethodName");

                if (methodProp.stringValue == "<Choose>" || methodProp.stringValue.StartsWith("<Missing - "))
                    return lines;

				eventDelegate.target = targetProp.objectReferenceValue;
                eventDelegate.methodName = methodProp.stringValue;

                if (eventDelegate.isValid == false)
                    return lines;

                SerializedProperty paramArrayProp = element.FindPropertyRelative("mParameters");
                EventDelegate.Parameter[] ps = eventDelegate.parameters;

                if (ps != null)
                {
					int imax = ps.Length;
                    paramArrayProp.arraySize = imax;
                    for (int i = 0; i < imax; i++)
                    {
                        EventDelegate.Parameter param = ps[i];

                        lines += lineHeight;

                        SerializedProperty paramProp = paramArrayProp.GetArrayElementAtIndex(i);
                        SerializedProperty objProp = paramProp.FindPropertyRelative("obj");

                        bool useManualValue = paramProp.FindPropertyRelative("paramRefType").enumValueIndex == (int)ParameterType.Value;

                        if (useManualValue)
                        {
                            if (param.expectedType == typeof(string) || param.expectedType == typeof(int) ||
                                param.expectedType == typeof(float) || param.expectedType == typeof(double) ||
                                param.expectedType == typeof(bool) || param.expectedType.IsEnum ||
                                param.expectedType == typeof(Color))
                            {
                                continue;
                            }

                            if (param.expectedType == typeof(Vector2) || param.expectedType == typeof(Vector3) || param.expectedType == typeof(Vector4))
                            {
                                //TODO: use minimalist method
                                lines += 2f;
                                continue;
                            }
                        }

                        Object obj = objProp.objectReferenceValue;

                        if (obj == null)
                            continue;

                        Type type = obj.GetType();

                        GameObject selGO = null;
                        if (type == typeof(GameObject))
                            selGO = obj as GameObject;
                        else if (type.IsSubclassOf(typeof(Component)))
                            selGO = (obj as Component).gameObject;

                        if (selGO != null)
                            lines += lineHeight;
                    }
                }

                return lines - lineHeight/2;
            };
        }

        return list;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
        if(!mShowList)
            return lineHeight;

        if (list == null)
            list = getList(property.FindPropertyRelative("List"));

		if (list == null)
			return 0;
	    return list.GetHeight();
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
        if(list == null)
        {
            var listProperty = property.FindPropertyRelative("List");

            list = getList(listProperty);
        }

        if(list != null)
        {
            list.drawHeaderCallback = rect =>
            {
                rect.x += 10;
                mShowList = EditorGUI.Foldout(rect, mShowList, property.name, true);
            };

            list.displayAdd = mShowList;
            list.displayRemove = mShowList;

            if(mShowList)
            {
                list.DoList(position);
            }
            else
            {
                if (Event.current.type == EventType.Repaint)
                {
                    headerBackground.Draw(position, false, false, false, false);
                }

                position.x += 16;
                mShowList = EditorGUI.Foldout(position, mShowList, property.name, true);
            }
        }
        
	}
}