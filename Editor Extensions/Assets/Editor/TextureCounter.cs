// BEGIN editor_include
using UnityEngine;
using System.Collections;
// BEGIN editor_include_highlight
using UnityEditor;
// END editor_include_highlight

// END editor_include

// BEGIN editor_window
public class TextureCounter : EditorWindow {

	// BEGIN editor_window_mode
	private enum Mode {
		ManualLayoutExample,
		ControlSampler,
		TextureCounter
	}

	private static Mode mode = Mode.TextureCounter;
	// END editor_window_mode

	// BEGIN example_list_current_selection_var
	private int selectedSizeIndex = 0;
	// END example_list_current_selection_var

	// BEGIN example_enum
	private enum DamageType {
		Fire,
		Frost,
		Electric,
		Shadow
	}

	private DamageType damageType;
	// END example_enum

	// BEGIN editor_window_gui_textfield_backing_store
	private string stringValue;
	// END editor_window_gui_textfield_backing_store

	// BEGIN editor_window_gui_special_fields_backing_stores
	private int intValue;
	
    private float floatValue;
	
    private Vector2 vector2DValue;
	
    private Vector3 vector3DValue;
	// END editor_window_gui_special_fields_backing_stores

	// BEGIN editor_window_gui_minmaxslider_backing_store
	private float minFloatValue;
	private float maxFloatValue;
	// END editor_window_gui_minmaxslider_backing_store

    // BEGIN editor_window_gui_init
	[MenuItem("Window/Texture Counter")]
	public static void Init() {
		var window = EditorWindow.GetWindow<TextureCounter>("Texture Counter");
        // Stops this window from being unloaded when a 
		// new scene is loaded
		DontDestroyOnLoad(window);
	}
    // END editor_window_gui_init

	// BEGIN editor_window_gui_scrollview_backing_store
	private Vector2 scrollPosition;
	// END editor_window_gui_scrollview_backing_store

	// BEGIN editor_window_ongui
	private void OnGUI() {
	// END editor_window_ongui

		// Editor GUI goes here

		// BEGIN editor_window_gui_auto_placement
		using (var verticalArea = new EditorGUILayout.VerticalScope()) {
		// END editor_window_gui_auto_placement

			// BEGIN editor_window_mode
			if (mode == Mode.ManualLayoutExample) {
			// END editor_window_mode
			// BEGIN editor_window_gui_manual_placement
            GUI.Label(                      // <1>
                    new Rect(50,50,100,20), // <2>
                    "This is a label!"      // <3>
            );                              
			// END editor_window_gui_manual_placement
			// BEGIN editor_window_mode
			} else if (mode == Mode.ControlSampler) {
			// END editor_window_mode

                // BEGIN editor_window_gui_scrollview
                using (var scrollView = new EditorGUILayout.ScrollViewScope(this.scrollPosition)) {

                    this.scrollPosition = scrollView.scrollPosition;

                // END editor_window_gui_scrollview

                	// BEGIN editor_window_gui_auto_placement_labels
                	// BEGIN editor_window_gui_auto_placement
                	GUILayout.Label("These");
                	// BEGIN editor_window_gui_label
                	GUILayout.Label("Labels");
                	// END editor_window_gui_label
                	GUILayout.Label("Will be shown");
                	GUILayout.Label("On top of each other");
                	// END editor_window_gui_auto_placement
                	// END editor_window_gui_auto_placement_labels

					// BEGIN editor_window_gui_space
					EditorGUILayout.Space();
					// END editor_window_gui_space

					// BEGIN editor_window_gui_labelfield
					EditorGUILayout.LabelField("Item 1", "Item 2");
					// END editor_window_gui_labelfield

					// BEGIN editor_window_gui_list
					var sizes = new string[] {"small","medium","large"};

					selectedSizeIndex = EditorGUILayout.Popup(selectedSizeIndex, sizes);
					// END editor_window_gui_list
                    EditorGUILayout.LabelField("Current selected size is " + sizes[selectedSizeIndex]);

					// BEGIN editor_window_gui_list_enums
					damageType = (DamageType)EditorGUILayout.EnumPopup(damageType);
					// END editor_window_gui_list_enums

					// BEGIN editor_window_gui_button
					var buttonClicked = GUILayout.Button("Click me!");
					if (buttonClicked) {
						Debug.Log("The custom window's button was clicked!");
					}
					// END editor_window_gui_button

					// BEGIN editor_window_gui_textfield
					this.stringValue = EditorGUILayout.TextField(this.stringValue);
					// END editor_window_gui_textfield

					// BEGIN editor_window_gui_textarea
					this.stringValue = EditorGUILayout.TextArea(
						this.stringValue, 
						GUILayout.Height(80)
					);
					// END editor_window_gui_textarea



                    // BEGIN noshow
					EditorGUILayout.LabelField("This text field won't work properly:");
                    // END noshow
					// BEGIN editor_window_gui_textarea_BROKEN
					string textValue = "";
					
                    textValue = EditorGUILayout.TextField(textValue);
					// END editor_window_gui_textarea_BROKEN

                    // BEGIN noshow
					EditorGUILayout.PrefixLabel("Delayed text field:");
                    // END noshow
					// BEGIN editor_window_gui_textarea_delayed_textfield
					this.stringValue = EditorGUILayout.DelayedTextField(this.stringValue);
					// END editor_window_gui_textarea_delayed_textfield

					// BEGIN editor_window_gui_special_fields
					this.intValue = EditorGUILayout.IntField("Int", this.intValue);
					
                    this.floatValue = EditorGUILayout.FloatField("Float", this.floatValue);
					
                    this.vector2DValue = EditorGUILayout.Vector2Field("Vector 2D", this.vector2DValue);
					
                    this.vector3DValue = EditorGUILayout.Vector3Field("Vector 3D", this.vector3DValue);
					// END editor_window_gui_special_fields

					// BEGIN editor_window_gui_sliders
					var minIntValue = 0;
					var maxIntValue = 10;
					this.intValue = EditorGUILayout.IntSlider(this.intValue, minIntValue, maxIntValue);
					// END editor_window_gui_sliders

					// BEGIN editor_window_gui_minmaxslider
					var minLimit = 0;
					var maxLimit = 10;
					EditorGUILayout.MinMaxSlider(ref minFloatValue, ref maxFloatValue, minLimit, maxLimit);
					// END editor_window_gui_minmaxslider


				// BEGIN editor_window_gui_scrollview
                }
				// END editor_window_gui_scrollview
			// BEGIN editor_window_mode
			} else if (mode == Mode.TextureCounter) {
			// END editor_window_mode


				// BEGIN editor_window_gui_texture_counter
				using (var vertical = new EditorGUILayout.VerticalScope()) {
					// Get the list of all textures
					var paths = AssetDatabase.FindAssets("t:texture");

					// Get the count
					var count = paths.Length;

					// Show a label
					EditorGUILayout.LabelField("Texture Count", count.ToString());

				}

				// END editor_window_gui_texture_counter
			// BEGIN editor_window_mode
			}
			// END editor_window_mode

			// BEGIN editor_window_gui_mode
			//mode = (Mode) EditorGUILayout.EnumPopup(mode);
			// END editor_window_gui_mode


		// BEGIN editor_window_gui_auto_placement
		}			
		// END editor_window_gui_auto_placement

	// BEGIN editor_window_ongui
	}
	// END editor_window_ongui


}
// END editor_window