using UnityEngine;
using System.Collections;

// BEGIN 2d_mainmenu
using UnityEngine.SceneManagement;

// Manages the main menu.
public class MainMenu : MonoBehaviour {

	// The name of the scene that contains the game itself.
	public string sceneToLoad;

	// The UI component that contains the "Loading..." text.
	public RectTransform loadingOverlay;

	// Represents the scene background loading.
	// This is used to control when the scene should switch over.
	AsyncOperation sceneLoadingOperation;

	// On start, begin loading the game.
	public void Start() {

		// Ensure the 'loading' overlay is invisible
		loadingOverlay.gameObject.SetActive(false);

		// Begin loading in the scene in the background...
		sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);

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


}
// END 2d_mainmenu
