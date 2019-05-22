using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class CountdownStarterPresenter : MonoBehaviour {

	[SerializeField] private PlayableDirector playableStartGame;

	[Inject] private CountdownTimerModel model;

	private void Start() {
		StartCoroutine(StartCutsceneBeforeCountdown());	
	}

	private IEnumerator StartCutsceneBeforeCountdown() {
		LogUtil.PrintInfo(gameObject, this.GetType(), "Starting game...");

		playableStartGame.gameObject.SetActive(true);
		playableStartGame.enabled = true;
		playableStartGame.Play();

		yield return new WaitForSeconds((float) playableStartGame.duration);

		playableStartGame.Stop();
		playableStartGame.enabled = false;
		playableStartGame.gameObject.SetActive(false);

		model.StartGameCountdown();
		Destroy(this);
	}

}
