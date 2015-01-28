using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string sceneToLoad;

	AsyncOperation sceneLoadingOperation;

	bool loadSceneWhenReady = false;

	public RectTransform loadingOverlay;

	public void Start() {

		loadingOverlay.gameObject.SetActive(false);

		StartCoroutine("LoadSceneInBackground");
	}

	public void Update() {
		if (sceneLoadingOperation != null) {

			if (loadSceneWhenReady) {
				sceneLoadingOperation.allowSceneActivation = true;
			}

		}
	}

	IEnumerator LoadSceneInBackground() {

		sceneLoadingOperation = Application.LoadLevelAsync(sceneToLoad);
		sceneLoadingOperation.allowSceneActivation = false;
		yield return sceneLoadingOperation;
	}

	public void LoadScene() {

		loadingOverlay.gameObject.SetActive(true);

		loadSceneWhenReady = true;

	}
}
