// BEGIN editor_include
using UnityEngine;
using System.Collections;
// BEGIN editor_include_highlight
using UnityEditor;
// END editor_include_highlight
// END editor_include

public class TextureCounter : EditorWindow {

	[MenuItem("Window/Texture Counter")]
	public static void Init() {
		var window = EditorWindow.GetWindow<TextureCounter>();

		// Stops this window from being unloaded when a 
		// new scene is loaded
		DontDestroyOnLoad(window);
	}

	private void OnGUI() {

		EditorGUILayout.BeginVertical();
		GUILayout.Button("what up");
		EditorGUILayout.EndVertical();
	}

}
