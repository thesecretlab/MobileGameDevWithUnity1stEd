// BEGIN color_changer_editor
using UnityEngine;
using System.Collections;
using System.Collections.Generic; // needed for Dictionary
using UnityEditor;

// BEGIN inspector_define
// This is an editor for RuntimeColorChangers
[CustomEditor(typeof(RuntimeColorChanger))]
// It can handle editing multiple things at once
[CanEditMultipleObjects]
class RuntimeColorChangerEditor : Editor {
// END inspector_define

    // BEGIN inspector_variables
	// A collection of string-color pairs
	private Dictionary<string, Color> colorPresets;

	// Represents the "color" property on all selected objects
	private SerializedProperty colorProperty;
    // END inspector_variables

    // Called when the editor first appears
    // BEGIN inspector_onenable
    public void OnEnable() {

        // Set up the list of color presets
        colorPresets = new Dictionary<string, Color>();

        colorPresets["Red"] = Color.red;
        colorPresets["Green"] = Color.green;
        colorPresets["Blue"] = Color.blue;
        colorPresets["Yellow"] = Color.yellow;
        colorPresets["White"] = Color.white;

        // Get the property from the object(s) that are currently selected
        colorProperty = serializedObject.FindProperty("color");
    }
    // END inspector_onenable

    // Called to draw the GUI in the Inspector
    // BEGIN inspector_ongui
    public override void OnInspectorGUI ()
    {
        // Ensure that the serializedObject is up to date
        serializedObject.Update();
        // END inspector_ongui

        // Start a vertical list of controls
        // BEGIN inspector_controls
        using (var area = new EditorGUILayout.VerticalScope()) {

        	// For each color in the preset list..
        	foreach (var preset in colorPresets) {

        		// Show a button
        		var clicked = GUILayout.Button(preset.Key);

        		// If it was clicked, update the property
        		if (clicked) {
        			colorProperty.colorValue = preset.Value;
        		}
        	}

        	// Finally, show a field that allows for setting the color directly
        	EditorGUILayout.PropertyField(colorProperty);
        }
        // END inspector_controls

        // BEGIN inspector_apply
        // Apply any property that was changed
        serializedObject.ApplyModifiedProperties();
        // END inspector_apply
    }
}
// END color_changer_editor