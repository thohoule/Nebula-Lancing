using UnityEngine;
using UnityEditor;

namespace TeaSteep
{
    //Source: https://discussions.unity.com/t/default-editor-enum-as-flags/75023/4

    [CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            _property.intValue = EditorGUI.MaskField(_position, _label, _property.intValue, _property.enumNames);
        }
    }
}
