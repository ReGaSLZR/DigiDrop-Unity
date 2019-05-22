using System.Collections;
using UnityEngine;
using UniRx;
using Zenject;

public class BasketPresenter : MonoBehaviour {

	[Header("Values and States")]
	[Tooltip("These values correspond to the state Texture below.")]
	[SerializeField] private int[] basketStateValues;
	[SerializeField] private Sprite[] basketStateTextures;

	[Header("Delays")]
	[SerializeField] private float sparklesFrameDelay = 0.1f;
	[SerializeField] private float sparklesFadeOutDelay = 2f;

	[Header("Containers")]
	[SerializeField] private SpriteRenderer basketStateContainer;
	[SerializeField] private SpriteRenderer basketSparkles;

	[Inject]
	private CatcherModel model;

	private void Awake() {
		if(basketStateTextures == null || basketStateTextures.Length == 0) {
			LogUtil.PrintError(gameObject, GetType(), 
				"NO basketStates Textures set! You're gonna have problems later on.");
		}		
	}

	private void Start() {
		InitializePresenter();

		basketStateContainer.sprite = basketStateTextures[0];
		HideBasketSparkles();
	}

	private void InitializePresenter() {

		model.reactiveCoinsCaught
			.Select(coinCount => {

				for(int x=basketStateValues.Length-1; x>0; x--) {
					if(((x == (basketStateValues.Length-1)) && (coinCount > basketStateValues[x])) || 
						(coinCount >= basketStateValues[x] && coinCount < basketStateValues[x+1])) {
						return basketStateTextures[x];
					}
				}

				return basketStateTextures[0];
			})
			.Subscribe(texture => {
				StartCoroutine(ShowSparkles());
				basketStateContainer.sprite = texture;
			})
			.AddTo(this);

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

}
