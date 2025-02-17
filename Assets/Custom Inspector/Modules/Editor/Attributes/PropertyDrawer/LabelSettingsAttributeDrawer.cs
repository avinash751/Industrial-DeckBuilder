using CustomInspector.Extensions;
using UnityEditor;
using UnityEngine;

namespace CustomInspector.Editor
{
    [CustomPropertyDrawer(typeof(LabelSettingsAttribute))]
    public class LabelSettingsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = PropertyValues.ValidateLabel(label, property); // validate first, so text changes stay afterwards

            LabelSettingsAttribute la = (LabelSettingsAttribute)attribute;

            if (la.newName != null)
            {
                label.text = la.newName;
            }

            DrawProperties.DrawLabelSettings(position, property, label, la.style.ToInteralStyle());
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return DrawProperties.GetPropertyHeight(label, property);
        }
    }
}