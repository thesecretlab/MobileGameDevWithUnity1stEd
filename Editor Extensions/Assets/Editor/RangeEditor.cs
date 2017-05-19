// BEGIN range_editor
using UnityEngine;
using System.Collections;

using UnityEditor;

// BEGIN propdraw_define
[CustomPropertyDrawer(typeof(Range))]
public class RangeEditor : PropertyDrawer {
// END propdraw_define

    // BEGIN propdraw_height
	// This property drawer will be 2 lines high - one for the slider,
	// and one for the text fields that let you change the values directly
	const int LINE_COUNT = 2;

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		// Return the number of pixels of height that this property takes up
		return base.GetPropertyHeight (property, label) * LINE_COUNT;
	}
    // END propdraw_height

    // BEGIN propdraw_ongui
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
    // END propdraw_ongui

        // BEGIN propdraw_properties
		// Get the objects that represent the fields inside this Range property
		var minProperty = property.FindPropertyRelative("min");
		var maxProperty = property.FindPropertyRelative("max");

		var minLimitProperty = property.FindPropertyRelative("minLimit");
		var maxLimitProperty = property.FindPropertyRelative("maxLimit");
        // END propdraw_properties

		// Any controls inside the PropertyScope will work correctly with 
		// prefabs - values that have been changed from the prefab
		// will be bold, and you can right-click on a value and choose to reset
		// it back to the prefab
        // BEGIN propdraw_propscope
		using (var propertyScope = new EditorGUI.PropertyScope(position, label, property)) {
        // END propdraw_propscope

            // Show the label; this method returns a rect
            // that stuff next to it can contain
            // BEGIN propdraw_prefix
            Rect sliderRect = EditorGUI.PrefixLabel(position, label);
            // END propdraw_prefix

            // Construct rectangles for each of the controls:

            // Calculate how big a single line is
            // BEGIN propdraw_rects
            var lineHeight = position.height / LINE_COUNT;

            // The slider needs to be one line high
            sliderRect.height = lineHeight;

            // The area for the two fields is the same shape as
            // the slider, but shifted down one line
            var valuesRect = sliderRect;
            valuesRect.y += sliderRect.height;

            // Work out rects for the two text fields
            var minValueRect = valuesRect;
            minValueRect.width /= 2.0f;

            var maxValueRect = valuesRect;
            maxValueRect.width /= 2.0f;
            maxValueRect.x += minValueRect.width;
            // END propdraw_rects

            // Get the float values out
            // BEGIN propdraw_prop_values
            var minValue = minProperty.floatValue;
            var maxValue = maxProperty.floatValue;
            // END propdraw_prop_values

            // Start a change check - we do this to correctly support
            // multi-object editing
            // BEGIN propdraw_changecheckstart
            EditorGUI.BeginChangeCheck();
            // END propdraw_changecheckstart

            // Show the slider
            // BEGIN propdraw_slider
            EditorGUI.MinMaxSlider(
            	sliderRect,
            	ref minValue,
            	ref maxValue, 
            	minLimitProperty.floatValue, 
            	maxLimitProperty.floatValue
            );
            // END propdraw_slider

            // Show the fields
            // BEGIN propdraw_fields
            minValue = EditorGUI.FloatField(minValueRect, minValue);
            maxValue = EditorGUI.FloatField(maxValueRect, maxValue);
            // END propdraw_fields

            // Was a value changed?
            // BEGIN propdraw_changecheckend
            var valueWasChanged = EditorGUI.EndChangeCheck();
            // END propdraw_changecheckend

            // BEGIN propdraw_storeprops
            if (valueWasChanged) {
            	// Store the modified values
            	minProperty.floatValue = minValue;
            	maxProperty.floatValue = maxValue;
            }
            // END propdraw_storeprops
		}


	}
}
// END range_editor