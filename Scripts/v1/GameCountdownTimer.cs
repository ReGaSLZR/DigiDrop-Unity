using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCountdownTimer : MonoBehaviour {

	public TextMeshProUGUI timerCounterText;
	public Image timerPanel;

	[SerializeField]
	private int timerInSeconds = 30;
	private Listener listener;

	private void Start() {
		if(PlayerPrefs.HasKey(PlayerPrefKey.TIME)) {
			timerInSeconds = PlayerPrefs.GetInt(PlayerPrefKey.TIME);
			timerCounterText.text = timerInSeconds.ToString("F0");
		}

		timerPanel.gameObject.SetActive(false);
	}

	public void StartGameCountdown(Listener listener) {
		this.listener = listener;
		StopAllCoroutines();
		StartCoroutine(StartGameCountdown());
	}

	private IEnumerator StartGameCountdown() {
		timerPanel.gameObject.SetActive(true);

		while(timerInSeconds > 0f) {
			yield return new WaitForSeconds(1f);
			timerInSeconds--;
			timerCounterText.text = timerInSeconds.ToString("F0");
		}

		timerPanel.gameObject.SetActive(false);

		if(listener != null) {
			listener.OnCountdownFinish();
		} else {
			LogUtil.PrintError(gameObject, GetType(), "Countdown finished BUT listener is null!");
		}
	}

	public interface Listener {
		void OnCountdownFinish();
	}

}
