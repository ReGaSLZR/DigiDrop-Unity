using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using TMPro;

public class AcceleroControl : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI debugLogAccelX;
	[SerializeField] private TextMeshProUGUI debugLogAccelWindow;

	[SerializeField] private float speedDamping = 10.0f;

	private Vector3 positionBuffer_Catcher;
	private Bounds screenBounds;

	private float transitionWindowMin = 0.1f;
	private float previousAccelX = 5f; //temp value

	private void InitializeConstants() {
		positionBuffer_Catcher = gameObject.transform.position;

		transitionWindowMin = PlayerPrefs.GetFloat(PlayerPrefKey.ACCEL_FRAME_WINDOW, 0.1f);
		debugLogAccelWindow.text = transitionWindowMin.ToString();
	}

	private void InitializeBounds() {
		float height = Camera.main.orthographicSize * 2;
		float width = height * Screen.width / Screen.height;

		screenBounds = new Bounds(Vector3.zero, new Vector3(width, height, 0));
	}

	private void Awake() {
		InitializeConstants();
		InitializeBounds();
	}

	private void OnEnable() {
		debugLogAccelX.gameObject.SetActive(true);
		debugLogAccelWindow.gameObject.SetActive(true);
	}

	private void OnDisable() {
		debugLogAccelX.gameObject.SetActive(false);
		debugLogAccelWindow.gameObject.SetActive(false);
	}

	private void Start() {
		Observable.EveryUpdate()
			.Select(_ => {
				float speed = Time.deltaTime * speedDamping; //get speed based on delta (fps rate)
				float tiltX = (Input.acceleration.x + speed);

				float clampedTiltX = Mathf.Clamp(tiltX, screenBounds.min.x, screenBounds.max.x);
				float roundedTiltX = ((float) Math.Round(clampedTiltX, 2));

				if((Mathf.Abs((Mathf.Abs(previousAccelX) - Mathf.Abs(roundedTiltX))) <= transitionWindowMin)) {
					return previousAccelX;
				} 

				previousAccelX = roundedTiltX;
				return roundedTiltX;
			})
			.TakeUntilDisable(this)
			.Subscribe(accelX => {
				positionBuffer_Catcher.x = accelX;

				gameObject.transform.position = positionBuffer_Catcher;
				debugLogAccelX.text = accelX.ToString();
			});
	}

}
