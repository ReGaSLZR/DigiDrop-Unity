using UnityEngine;

public class GyroControl : MonoBehaviour {

	private Quaternion rotationBuffer = new Quaternion(0, 0, 1, 0);

	/*
		NOTE: lines below were commented as GameDeviceControlStarter has taken over the checking of Gyroscope on device
	*/
//	private void Start() 
//	{
//		if(!IsGyroEnabled()) 
//		{
//			Destroy(this);
//		}
//	}

//	private bool IsGyroEnabled() 
//	{
//		if(SystemInfo.supportsGyroscope) 
//		{
//			Input.gyro.enabled = true;
//			return true;
//		}
//
//		Debug.LogError(this.GetType().Name + " Gyroscope is NOT supported by the device.");
//		return false;
//	}

	private void Update()
	{
		transform.localRotation = Input.gyro.attitude * rotationBuffer;
	}

}
