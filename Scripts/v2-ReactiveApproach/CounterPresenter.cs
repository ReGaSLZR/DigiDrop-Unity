using UnityEngine;
using UniRx;
using UniRx.Triggers;
using TMPro;
using Zenject;

public class CounterPresenter : MonoBehaviour {

	[Header("Text UI")]
	[SerializeField] private TextMeshProUGUI inGameScore;
	[SerializeField] private TextMeshProUGUI gameOverScore;
	[SerializeField] private TextMeshProUGUI timer;

	[Header("UI Colors")]
	[SerializeField] private Color colorTense;
	[SerializeField] private Color colorNormal;

	[Inject] private CountdownTimerModel modelCountdown;
	[Inject] private CatcherModel modelCatcher;

	private NetworkModelTest network;

	private void Awake() {
		network = new NetworkModelTest();
	}

	private void Start () {
		InitializePresenter();
		TestNetwork();
	}

	private void InitializePresenter() {
		timer.color = colorNormal;

		modelCountdown.reactiveIsTenseTime.Where(isTense => (isTense == true))
			.Subscribe(_ => timer.color = colorTense)
			.AddTo(this);

		modelCountdown.reactiveCountdown
			.Subscribe(timeLeft => timer.text = timeLeft.ToString("F0"))
			.AddTo(this);		

		modelCatcher.reactiveCoinsCaught
			.Subscribe(coinsCaught => {
				inGameScore.text = coinsCaught.ToString();
				gameOverScore.text = coinsCaught.ToString();
			})
			.AddTo(this);
	}

	private void TestNetwork() {
		network.GetUser_Failure();
		network.GetUser_Success();

		network.PostLogin_Failure();
		network.PostLogin_Success();
	}

}
