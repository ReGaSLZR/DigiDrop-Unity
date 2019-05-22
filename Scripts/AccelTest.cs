using System.Collections;
using UnityEngine;
using TMPro;

public class AccelTest : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI textUpdate;

//	private float defaultX;
//	private float defaultY;
//	private float defaultZ;

	private Vector3 positionBuffer;

	private void Start() {
		positionBuffer = gameObject.transform.position;

//		defaultX = gameObject.transform.position.x;
//		defaultY = gameObject.transform.position.y;
//		defaultZ = gameObject.transform.position.z;
	}

	void Update () 
	{
		float speed = Time.deltaTime * 1.0f; //get speed based on delta (fps rate)
		positionBuffer.x += speed + Input.acceleration.x;

		textUpdate.text = positionBuffer.x.ToString();
		transform.position = positionBuffer;
	

	}

}
