using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

/*
	NOTE: make sure the Collider of the gameObject is a Trigger
*/
[RequireComponent(typeof(Collider))]
public class CatcherPresenter : MonoBehaviour {
	
	[SerializeField] private GameObject prefabCoinCaughtEffect;

	[Inject] private CatcherModel modelCatcher;
	[Inject] private CountdownTimerModel modelCountdown;

	private void Start() {
		InitializePresenter();
	}

	private void InitializePresenter() {

		this.OnTriggerEnterAsObservable()
			.Where(otherCollider => otherCollider.tag.Equals(Tag.COIN))
			.Subscribe(otherColllider => {
				Instantiate(prefabCoinCaughtEffect, otherColllider.gameObject.transform.position, Random.rotation);
				modelCatcher.OnTriggerCoinCatch();
				Destroy(otherColllider.gameObject);
			})
			.AddTo(this);
				
		//NOTE: we need this condition to prevent firing the content when the coin count has been reset
		modelCountdown.reactiveIsOver
			.Where(isOver => (isOver == true))
			.Subscribe(_ => {
				this.gameObject.SetActive(false);
			})
			.AddTo(this);

		modelCountdown.reactiveHasStarted
			.Where(hasStarted => (hasStarted == true))
			.Subscribe(_ => {
				this.gameObject.SetActive(true);
			})
			.AddTo(this);

	}

}