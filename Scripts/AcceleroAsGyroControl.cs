using System.Collections;
using UnityEngine;

/*
	NOTE: this class uses Acclerometer as a substitute to Gyroscope in case the device doesn't have Gyro
*/
public class AcceleroAsGyroControl : MonoBehaviour {

	[SerializeField] private float rotationDamping = 1.0f;
	private Quaternion rotationBuffer;

	private void Start () {
		rotationBuffer = transform.rotation;
	}

	private void Update () {
		float rotationSpeed = Time.deltaTime * rotationDamping; //get speed based on delta (fps rate)
		rotationBuffer.x += Input.acceleration.x * rotationSpeed;
		rotationBuffer.y += Input.acceleration.y * rotationSpeed;

		transform.rotation = rotationBuffer;
	}

}
