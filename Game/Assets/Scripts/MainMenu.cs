using UnityEngine;
using System.Collections;

// Manages the main menu.
public class MainMenu : MonoBehaviour {

	// The name of the scene that contains the game itself.
	public string sceneToLoad;

	// The UI component that contains the "Loading..." text.
	public RectTransform loadingOverlay;

	// If we have a Unity Pro license, we can load scenes in the background.
	// All of the code from the following line to the '#else' line below will only
	// be used if you have Unity Pro.

	// (If you have Unity Pro, and want to see what the non-Pro code does,
	// change UNITY_PRO_LICENSE to !UNITY_PRO_LICENSE.)
#if UNITY_PRO_LICENSE

	// Represents the scene background loading.
	// This is used to control when the scene should switch over.
	AsyncOperation sceneLoadingOperation;

	// On start, begin loading the game.
	public void Start() {

		// Ensure the 'loading' overlay is invisible
		loadingOverlay.gameObject.SetActive(false);

		// Begin loading in the scene in the background...
		sceneLoadingOperation = Application.LoadLevelAsync(sceneToLoad);

		// ...but don't actually switch to the new scene until we're ready.
		sceneLoadingOperation.allowSceneActivation = false;


	}

	// Called when the New Game button is tapped.
	public void LoadScene() {

		// Make the 'Loading' overlay visible
		loadingOverlay.gameObject.SetActive(true);

		// Tell the scene loading operation to switch scenes
		// when it's done loading.
		sceneLoadingOperation.allowSceneActivation = true;

	}
#else 
	// If we don't have a Unity Pro license, this version of the code will
	// be used.

	public void Start() {
		// Ensure the 'loading' overlay is invisible
		loadingOverlay.gameObject.SetActive(false);
	}

	// Called when the New Game button is tapped.
	public void LoadScene() {

		// Make the 'Loading' overlay visible.
		loadingOverlay.gameObject.SetActive(true);

		// Tell Unity to begin loading the scene.
		// The new scene will automatically appear when loading is complete.
		Application.LoadLevel(sceneToLoad);		
	}
#endif


}
