using UnityEngine;
using UnityEngine.UI;

/*
	NOTE: taken from N3K EN https://www.youtube.com/watch?v=c6NXkZWXHnc
*/
public class DeviceCameraAccessor : MonoBehaviour {

	private bool isCamAvailable;
	private WebCamTexture backCamera;
	private Texture defaultBackground;

	[SerializeField] private RawImage background;
	[SerializeField] private AspectRatioFitter fit;

	private void Start() 
	{
		defaultBackground = background.texture;
		WebCamDevice[] devices = WebCamTexture.devices;

		if(devices.Length == 0) {
			LogUtil.PrintInfo(gameObject, GetType(), "No camera detected");

			isCamAvailable = false;
			return;
		} 

		for(int i=0; i<devices.Length; i++) {
			if(!devices[i].isFrontFacing) {
				backCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
			}
		}

		if(backCamera == null) {
			LogUtil.PrintError(gameObject, GetType(), "Unable to find back camera");
			background.texture = defaultBackground;
			return;
		}

		backCamera.Play();
		background.texture = backCamera;

		isCamAvailable = true;
	}

	private void Update() 
	{
		if(!isCamAvailable) {
			return;
		}

		float ratio = (float) backCamera.width / (float)backCamera.height;
		fit.aspectRatio = ratio;

		float scaleY = backCamera.videoVerticallyMirrored ? -1 : 1f;
		background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

		background.rectTransform.localEulerAngles = new Vector3(0, 0, -backCamera.videoRotationAngle);
	}

}
