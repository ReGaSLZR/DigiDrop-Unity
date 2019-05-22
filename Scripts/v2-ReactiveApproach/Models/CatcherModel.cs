using UniRx;

public class CatcherModel {

	public ReactiveProperty<int> reactiveCoinsCaught { get; private set; }

	public CatcherModel() {
		reactiveCoinsCaught = new ReactiveProperty<int>(0);
	}

	public void OnTriggerCoinCatch() {
		reactiveCoinsCaught.Value++;
	}

}
