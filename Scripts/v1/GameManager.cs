using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour, GameCountdownTimer.Listener {

	public Text scoreCounter;
	public Image panelScore;
	public TextMeshProUGUI gameOverScoreCounter;
	public Image panelGameOver;
	public Image panelLoading;

	public AudioSource audioSourceCoinTaken;
	public BasketStateManager basketStateManager;
	public BGMRandomizer bgmRandomizer;
	public PlayableDirector countdownPlayable;

	public GameCountdownTimer countdownTimer;
	public CoinSpawner coinSpawner;
	public CoinCatcher coinCatcher;

	private int coinsCaught;
	private int coinsUNCaught;

	public void OnCountdownFinish() {
		bgmRandomizer.StopBGM();
		coinSpawner.StopSpawning();
		panelScore.gameObject.SetActive(false);

		gameOverScoreCounter.text = coinsCaught.ToString();
		panelGameOver.gameObject.SetActive(true);
		coinCatcher.gameObject.SetActive(false);
		panelLoading.gameObject.SetActive(false);
	}

	private void Awake() {
		
	}

	private void Start () {
		coinsCaught = 0;
		coinsUNCaught = 0;

		countdownPlayable.Stop();
		countdownPlayable.enabled = false;
		countdownPlayable.gameObject.SetActive(false);

		StartCoroutine(StartGame());
	}

	public void RestartGame() {
		panelLoading.gameObject.SetActive(true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void StoreCoinCaught() 
	{
		coinsCaught++;
		UpdateUI();
		basketStateManager.UpdateBasket(coinsCaught);
		PlayCaughtCoinSFX();
	}

	public void StoreCoinUNCaught()
	{
		coinsUNCaught++;
//		LogUtil.PrintInfo(gameObject, GetType(), "UNcaught Coins Counter:" + coinsUNCaught);
	}

	private void UpdateUI()
	{
//		LogUtil.PrintInfo(gameObject, GetType(), "UpdateUI Coins Caught: " + coinsCaught);
		scoreCounter.text = coinsCaught.ToString();
	}

	private void PlayCaughtCoinSFX() 
	{
		audioSourceCoinTaken.Play();
	}

	private IEnumerator StartGame() {
		panelLoading.gameObject.SetActive(false);
		coinCatcher.gameObject.SetActive(true);
		panelScore.gameObject.SetActive(false);
		panelGameOver.gameObject.SetActive(false);

		countdownPlayable.gameObject.SetActive(true);
		countdownPlayable.enabled = true;
		countdownPlayable.Play();
		yield return new WaitForSeconds((float)countdownPlayable.duration);

		countdownPlayable.gameObject.SetActive(false);
		bgmRandomizer.PlayRandomBGM();
		panelScore.gameObject.SetActive(true);
		countdownTimer.StartGameCountdown(this);
		coinSpawner.StartSpawning();
	}

}
