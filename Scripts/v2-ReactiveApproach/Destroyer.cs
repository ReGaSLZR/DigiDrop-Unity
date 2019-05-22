using UnityEngine;
using UniRx;
using UniRx.Triggers;

[RequireComponent(typeof(Collider))]
public class Destroyer : MonoBehaviour {

	private void Start() {
		this.OnTriggerEnterAsObservable()
			.Where(otherCollider => otherCollider.tag.Equals(Tag.COIN))
			.Subscribe(otherCollider => {
				Destroy(otherCollider.gameObject);
			})
			.AddTo(this);
	}

}
