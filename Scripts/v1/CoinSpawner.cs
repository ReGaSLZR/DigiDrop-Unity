using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

	public GameObject coinObject;
	[SerializeField] private int spawnRadius = 3;

	[Header("Coins per Wave")]
	[SerializeField] private int coinsPerWaveMin = 10;
	[SerializeField] private int coinsPerWaveMax = 20;

	[Header("Coin Wave Intervals (in Seconds)")]
	[SerializeField] private int coinWaveIntervalMin = 1;
	[SerializeField] private int coinWaveIntervalMax = 4;

	private bool shouldSpawn;

	private void Awake() 
	{
		if(coinObject == null) {
			LogUtil.PrintError(gameObject, GetType(), "There is NO coinObject set.");
			shouldSpawn = false;
			return;
		}	
	}

	private void Start() {
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

	public void StartSpawning() 
	{
		LogUtil.PrintInfo(gameObject, GetType(), "Spawning coins now");
		shouldSpawn = true;
		StartCoroutine(SpawnCoinsRandomly());
	}

	private IEnumerator SpawnCoinsRandomly() {
		if(coinObject != null) {
			while(shouldSpawn) {
				SpawnCoinWave();
				yield return new WaitForSeconds(Random.Range(coinWaveIntervalMin, coinWaveIntervalMax));
			}
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
		
	public void StopSpawning() {
		shouldSpawn = false;
		LogUtil.PrintInfo(gameObject, GetType(), "Stopped spawning coins.");
		StopAllCoroutines();
	}


}
