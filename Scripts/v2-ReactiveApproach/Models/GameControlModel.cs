using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlModel {

	public void OnTriggerConfig() {
		SceneManager.LoadScene(0);
	}

	public void OnTriggerRestart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnTriggerQuit() {
		Application.Quit();
	}

}
