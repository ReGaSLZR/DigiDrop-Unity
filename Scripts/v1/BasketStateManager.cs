using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketStateManager : MonoBehaviour {

	public float sparklesFrameDelay = 0.1f;
	public float sparklesFadeOutDelay = 2f;

//	public int STATE_1 = 1;
//	public int STATE_2 = 5;
//	public int STATE_3 = 15;
//	public int STATE_4 = 25;
//	public int STATE_5 = 40;
//	public int STATE_6 = 60;

	[Header("Values and States")]
	[Tooltip("These values correspond to the state Texture below.")]
	public int[] basketStateValues;
	public Texture[] basketStates;

	[Header("Containers")]
	public RawImage basketImage;
	public RawImage basketSparkles;

	private void Awake () {
		if(basketStates == null || basketStates.Length == 0) {
			LogUtil.PrintError(gameObject, GetType(), "NO basketStates set! You're gonna have problems later on.");
		}	
	}

	private void Start () {
		basketImage.texture = basketStates[0];
		HideBasketSparkles();
	}

	public void UpdateBasket(int coinCount) {
		StartCoroutine(ShowSparkles());
		ChangeBasketTexture(coinCount);
	}

	private IEnumerator ShowSparkles() {
		if(basketSparkles.color.a > 0f) {
			yield break;
		}
		else {
			float alpha = basketSparkles.color.a;

			while(alpha < 1f) {
				yield return new WaitForSeconds(sparklesFrameDelay);
				alpha += sparklesFrameDelay;
				UpdateBasketSparkles(alpha);
			}

			yield return new WaitForSeconds(sparklesFadeOutDelay);
			HideBasketSparkles();
		}
	}

	private void HideBasketSparkles() {
		Color kleur = basketSparkles.color;
		kleur.a = 0f;
		basketSparkles.color = kleur;
	}

	private void UpdateBasketSparkles(float alpha) {
		Color kleur = basketSparkles.color;
		kleur.a = alpha;
		basketSparkles.color = kleur;
	}
		
	private void ChangeBasketTexture(int coinCount) {
		bool hasChangedTexture = false;

		for(int x=basketStateValues.Length-1; x>0; x--) {
			if(((x == (basketStateValues.Length-1)) && (coinCount > basketStateValues[x])) || 
				(coinCount >= basketStateValues[x] && coinCount < basketStateValues[x+1])) {
				basketImage.texture = basketStates[x];
				hasChangedTexture = true;
				return;
			}
		}

		if(!hasChangedTexture) {
			basketImage.texture = basketStates[0];
		}

		//NOTE: the above LOCs of this method are the same as the commented LOCs below
//		if(coinCount >= STATE_6) {
//			basketImage.texture = basketStates[6];
//		}
//		else if(coinCount >= STATE_5 && coinCount < STATE_6) {
//			basketImage.texture = basketStates[5];
//		}
//		else if(coinCount >= STATE_4 && coinCount < STATE_5) {
//			basketImage.texture = basketStates[4];
//		}
//		else if(coinCount >= STATE_3 && coinCount < STATE_4) {
//			basketImage.texture = basketStates[3];
//		}
//		else if(coinCount >= STATE_2 && coinCount < STATE_3) {
//			basketImage.texture = basketStates[2];
//		}
//		else if(coinCount >= STATE_1 && coinCount < STATE_2) {
//			basketImage.texture = basketStates[1];
//		}
//		else {
//			basketImage.texture = basketStates[0];
//		}
	}

}
