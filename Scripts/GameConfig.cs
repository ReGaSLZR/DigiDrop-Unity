using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using TMPro;
using UnityEngine.SceneManagement;

public class GameConfig : MonoBehaviour {

	[Header("Input Fields")]
	[SerializeField] private TMP_InputField inputTime;
	[SerializeField] private TMP_InputField inputCoinWaveInterval_Min;
	[SerializeField] private TMP_InputField inputCoinWaveInterval_Max;
	[SerializeField] private TMP_InputField inputCoinsPerWave_Min;
	[SerializeField] private TMP_InputField inputCoinsPerWave_Max;
	[SerializeField] private TMP_InputField inputAccelWindow;

	[Header("Input Label/s")]
	[SerializeField] private TextMeshProUGUI inputDeviceControlLabel;

	[Header("Buttons")]
	[SerializeField] private Button buttonPlay;
	[SerializeField] private Button buttonControl;

	[Header("Panels")]
	[SerializeField] private Image panelInputs;
	[SerializeField] private Image panelLoading;

	private void Awake() {
		inputDeviceControlLabel.text = PlayerPrefValue.DEVICE_INPUT_CONTROL_CHOICE_ACCEL;
		ShowInputForm();
	}

	private void Start() {
		buttonControl.OnClickAsObservable()
			.TakeUntilDisable(this)
			.Subscribe(_ => {
				OnClickDeviceInput();
			});

		buttonPlay.OnClickAsObservable()
			.TakeUntilDisable(this)
			.Subscribe(_ => {
				StartGame();
			});
	}

	private void OnClickDeviceInput() {
		if(inputDeviceControlLabel.text.Equals(PlayerPrefValue.DEVICE_INPUT_CONTROL_CHOICE_GYRO)) {
			inputDeviceControlLabel.text = PlayerPrefValue.DEVICE_INPUT_CONTROL_CHOICE_ACCEL;
		} else {
			inputDeviceControlLabel.text = PlayerPrefValue.DEVICE_INPUT_CONTROL_CHOICE_GYRO;
		}
	}

	private void StartGame() {
		LogUtil.PrintInfo(gameObject, GetType(), "Starting game...");
		StoreInput();
		ShowLoading();

		SceneManager.LoadScene(1);
	}

	private void ShowInputForm() {
		panelLoading.gameObject.SetActive(false);
		panelInputs.gameObject.SetActive(true);
		buttonPlay.gameObject.SetActive(true);
	}

	private void ShowLoading() {
		panelLoading.gameObject.SetActive(true);
		panelInputs.gameObject.SetActive(false);
		buttonPlay.gameObject.SetActive(false);
	}

	private void StoreInput() {
		PlayerPrefs.SetInt(PlayerPrefKey.TIME, int.Parse(inputTime.text));
		PlayerPrefs.SetInt(PlayerPrefKey.COINS_PER_WAVE_MAX, int.Parse(inputCoinsPerWave_Max.text));
		PlayerPrefs.SetInt(PlayerPrefKey.COINS_PER_WAVE_MIN, int.Parse(inputCoinsPerWave_Min.text));
		PlayerPrefs.SetInt(PlayerPrefKey.COIN_WAVE_INTERVAL_MAX, int.Parse(inputCoinWaveInterval_Max.text));
		PlayerPrefs.SetInt(PlayerPrefKey.COIN_WAVE_INTERVAL_MIN, int.Parse(inputCoinWaveInterval_Min.text));

		PlayerPrefs.SetString(PlayerPrefKey.DEVICE_INPUT_CONTROL_CHOICE, inputDeviceControlLabel.text);

		PlayerPrefs.SetFloat(PlayerPrefKey.ACCEL_FRAME_WINDOW, float.Parse(inputAccelWindow.text));

		PlayerPrefs.Save();
	}

}
