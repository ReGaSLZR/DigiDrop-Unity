using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	NOTE: use DeviceCameraAccessor instead
*/
public class CameraReflector : MonoBehaviour {

	[SerializeField] private GameObject plane;

	IEnumerator Start() 
	{
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);		
		WebCamTexture camTexture = new WebCamTexture();
		plane.GetComponent<Renderer>().material.mainTexture = camTexture;
		camTexture.Play();
	}

}
