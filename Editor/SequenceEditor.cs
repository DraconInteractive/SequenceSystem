﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DI_Sequences
{ 
    [CustomPropertyDrawer(typeof(Sequence))]
    public class SequenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty prop = property.FindPropertyRelative("actions");

            float propHeight = EditorGUI.GetPropertyHeight(prop, true);
            Rect propRect = new Rect(position.x, position.y, position.width, propHeight);
            
            EditorGUI.PropertyField(propRect, prop, true);
            if (prop.isExpanded)
            {
                Rect enumRect = new Rect(position.x, position.y + propHeight + EditorGUIUtility.standardVerticalSpacing, position.width, 50);

                var type = (Sequence.ActionType)EditorGUI.EnumPopup(enumRect, "Create Sequence: ", Sequence.ActionType.SelectType);
                if (type != Sequence.ActionType.SelectType)
                {
                    switch (type)
                    {
                        case Sequence.ActionType.SelectType:
                            break;
                        case Sequence.ActionType.Debug:
                            SerializedProperty newAction = prop.GetArrayElementAtIndex(prop.arraySize++);
                            newAction.managedReferenceValue = new DebugAction();
                            break;
                        case Sequence.ActionType.MoveTransform:
                            break;
                    }
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty prop = property.FindPropertyRelative("actions");

            return EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing + (prop.isExpanded ? EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing : 0);
        }
    }
}