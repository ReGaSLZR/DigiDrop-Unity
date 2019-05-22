using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDeviceControlStarter : MonoBehaviour {

	[SerializeField] private GyroControl gyroControl;
	[SerializeField] private AcceleroControl accelControl;

	[Header("Panels for Device Control")]
	[SerializeField] private Image panelGyro;
	[SerializeField] private Image panelAccel;
	[SerializeField] private Image panelErrorGyroAccel;

	private void Awake() {
		if(gyroControl == null || accelControl == null) {
			LogUtil.PrintError(gameObject, GetType(), "No GyroControl &/or AcceleroControl script assigned.");
			return;
		}

		DisableEverything();

		if(HasRequiredControls()) {
			ManageControls();	
		} else {
			panelErrorGyroAccel.gameObject.SetActive(true);	
		}
	}

	private void DisableEverything() {
		gyroControl.enabled = false;
		accelControl.enabled = false;

		panelGyro.gameObject.SetActive(false);
		panelAccel.gameObject.SetActive(false);
		panelErrorGyroAccel.gameObject.SetActive(false);
	}

	private void Start() {
		Destroy(this);
	}

	private bool HasRequiredControls() {
		return (SystemInfo.supportsAccelerometer || SystemInfo.supportsGyroscope);
	}

	private void ManageControls() {
		if(PlayerPrefs.HasKey(PlayerPrefKey.DEVICE_INPUT_CONTROL_CHOICE)) {
			if(PlayerPrefs.GetString(PlayerPrefKey.DEVICE_INPUT_CONTROL_CHOICE)
				.Equals(PlayerPrefValue.DEVICE_INPUT_CONTROL_CHOICE_GYRO)) {
				LogUtil.PrintInfo(gameObject, GetType(), "Control Chosen: GYRO | starting Gyroscope...");
				StartGyro();
			} else {
				StartAccel();
			}
		} else {
			LogUtil.PrintInfo(gameObject, GetType(), "No Input choice stored. Defaulting to Gyro control...");
			StartGyro();
		}
	}

	private void StartGyro() {
		if(SystemInfo.supportsGyroscope) {
			Input.gyro.enabled = true;
			LogUtil.PrintInfo(gameObject, GetType(), "Device Gyroscope has been started.");

			gyroControl.enabled = true;
			panelGyro.gameObject.SetActive(true);
		} else {
			LogUtil.PrintError(gameObject, GetType(), "Oh, no. Your device doesn't support Gyroscope.");
		}
	}

	private void StartAccel() {
		if(SystemInfo.supportsAccelerometer) {
			LogUtil.PrintInfo(gameObject, GetType(), "Device Accelerometer is supported. Switching to accel game control.");

			accelControl.enabled = true;
			panelAccel.gameObject.SetActive(true);
		} else {
			LogUtil.PrintError(gameObject, GetType(), "Nuuuu! Device doesn't support Accelerometer.");
		}
	}

}
