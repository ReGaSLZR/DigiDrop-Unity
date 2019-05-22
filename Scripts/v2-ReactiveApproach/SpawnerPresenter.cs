using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class SpawnerPresenter : MonoBehaviour {

	[SerializeField] private GameObject coinObject;
	[SerializeField] private int spawnRadius = 3;

	[Header("Coins per Wave")]
	[SerializeField] private int coinsPerWaveMin = 10;
	[SerializeField] private int coinsPerWaveMax = 20;

	[Header("Coin Wave Intervals (in Seconds)")]
	[SerializeField] private int coinWaveIntervalMin = 1;
	[SerializeField] private int coinWaveIntervalMax = 4;

	[Inject]
	private CountdownTimerModel model;

	private void Awake() {
		if(coinObject == null) {
			LogUtil.PrintError(gameObject, GetType(), "There is NO coinObject set.");
			return;
		}	

		InitializeConfigValues();
	}

	private void Start() {
		InitializePresenter();
	}

	private void InitializePresenter() {
		model.reactiveIsOver
			.Where(isOver => (isOver == true))
			.Subscribe(_ => {
				StopSpawning();
			})
			.AddTo(this);

		model.reactiveHasStarted
			.Where(hasStarted => (hasStarted == true))
			.Subscribe(_ => {
				StartSpawning();
			})
			.AddTo(this);
	}

	private void InitializeConfigValues() {
		if(PlayerPrefs.HasKey(PlayerPrefKey.COINS_PER_WAVE_MAX)) {
			coinsPerWaveMax = PlayerPrefs.GetInt(PlayerPrefKey.COINS_PER_WAVE_MAX);
		}

		if(PlayerPrefs.HasKey(PlayerPrefKey.COINS_PER_WAVE_MIN)) {
			coinsPerWaveMin = PlayerPrefs.GetInt(PlayerPrefKey.COINS_PER_WAVE_MIN);
		}

		if(PlayerPrefs.HasKey(PlayerPrefKey.COIN_WAVE_INTERVAL_MAX)) {
			coinWaveIntervalMax = PlayerPrefs.GetInt(PlayerPrefKey.COIN_WAVE_INTERVAL_MAX);
		}

		if(PlayerPrefs.HasKey(PlayerPrefKey.COIN_WAVE_INTERVAL_MIN)) {
			coinWaveIntervalMin = PlayerPrefs.GetInt(PlayerPrefKey.COIN_WAVE_INTERVAL_MIN);
		}
	}
	
	public void StartSpawning() {
		LogUtil.PrintInfo(gameObject, GetType(), "Spawning coins now...");	
		StartCoroutine(SpawnCoinsRandomly());
	}

	public void StopSpawning() {
		LogUtil.PrintInfo(gameObject, GetType(), "Stopped spawning coins...");
		StopAllCoroutines();
	}

	private IEnumerator SpawnCoinsRandomly() {
		while(coinObject != null) {
			SpawnCoinWave();
			yield return new WaitForSeconds(Random.Range(coinWaveIntervalMin, coinWaveIntervalMax));
		}	
	}

	private void SpawnCoinWave() {
		int coinsSpawned = 0;
		int coinsToSpawn = Random.Range(coinsPerWaveMin, coinsPerWaveMax);

		while(coinsSpawned < coinsToSpawn) 
		{
			Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
			randomPosition.y = transform.localPosition.y;
			Instantiate(coinObject, randomPosition, Random.rotation);

			coinsSpawned++;
		}
	}

}
