using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class GameStateControlPresenter : MonoBehaviour {

	[Header("GameOver Buttons")]
	[SerializeField] private Button buttonConfig;
	[SerializeField] private Button buttonQuit;
	[SerializeField] private Button buttonRestart;

	[Header("Panels")]
	[SerializeField] private Image panelLoading;
	[SerializeField] private Image inGameStatsPanel;
	[SerializeField] private Image gameOverPanel;

	[Inject] private CountdownTimerModel modelCountdown;
	[Inject] private GameControlModel modelControl;

	private void Start() {
		InitializeButtonOperations();
		InitializeGameStates();
	}

	private void InitializeButtonOperations() {
		buttonConfig.OnClickAsObservable()
			.TakeUntilDisable(this)
			.Subscribe(_ => {
				panelLoading.gameObject.SetActive(true);
				modelControl.OnTriggerConfig();
			});

		buttonQuit.OnClickAsObservable()
			.TakeUntilDisable(this)
			.Subscribe(_ => {
				modelControl.OnTriggerQuit();
			});

		buttonRestart.OnClickAsObservable()
			.TakeUntilDisable(this)
			.Subscribe(_ => {
				panelLoading.gameObject.SetActive(true);
				modelControl.OnTriggerRestart();
			});
		
	}

	private void InitializeGameStates() {
		modelCountdown.reactiveIsOver
			.Where(isOver => (isOver == true))
			.Subscribe(_ => {
				inGameStatsPanel.gameObject.SetActive(false);
				gameOverPanel.gameObject.SetActive(true);
			})
			.AddTo(this);	

		modelCountdown.reactiveHasStarted
			.Where(hasStarted => (hasStarted == true))
			.Subscribe(_ => {
				inGameStatsPanel.gameObject.SetActive(true);
				gameOverPanel.gameObject.SetActive(false);
			})
			.AddTo(this);
	}

}

