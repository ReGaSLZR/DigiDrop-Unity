using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System.Security;

public class Test : MonoBehaviour
{

	public IObservable<Vector2> Movement { get; private set; }

	public ReadOnlyReactiveProperty<bool> IsRunning { get; private set; }

	private void Awake ()
	{
		Movement = this.FixedUpdateAsObservable().Select(_ => {
			var x = Input.GetAxis("Horizontal");
			var y = Input.GetAxis("Vertical");

			return new Vector2(x, y).normalized;
		});

		IsRunning = this.UpdateAsObservable()
			.Select(_ => Input.GetButton("Fire3"))
			.Delay(System.TimeSpan.FromSeconds(1.0))
			.ToReadOnlyReactiveProperty();

	}

	private void Start() {
//		Movement.

		SecureString secString = new SecureString();

	}

}

