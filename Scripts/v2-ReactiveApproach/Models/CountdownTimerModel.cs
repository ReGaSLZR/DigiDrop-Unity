using UnityEngine;
using UniRx;

public class CountdownTimerModel {

	private const int DEFAULT_COUNTDOWN_TIMER_IN_SECONDS = 30;
	private const int DEFAULT_INTERVAL_TIMER_TICK = 1;
	private const int DEFAULT_TENSE_TIME_LEFT_IN_SECONDS = 10;

	public ReactiveProperty<int> reactiveCountdown { get; private set; }
	public ReactiveProperty<bool> reactiveIsTenseTime { get; private set; }
	public ReactiveProperty<bool> reactiveHasStarted { get; private set; }
	public ReactiveProperty<bool> reactiveIsOver { get; private set; }

	public CountdownTimerModel() {
		reactiveHasStarted = new ReactiveProperty<bool>(false);
		reactiveIsTenseTime = new ReactiveProperty<bool>(false);

		reactiveCountdown = Observable.Interval(System.TimeSpan.FromSeconds(DEFAULT_INTERVAL_TIMER_TICK))
			.Where(_ => (reactiveHasStarted.Value == true))
			.Select(_ => {
//				LogUtil.PrintInfo(this.GetType(), "Timer Update: " + timerInSeconds);
				return --(reactiveCountdown.Value);
			})
			.ToReactiveProperty();

		reactiveIsTenseTime = reactiveCountdown.Where(timeLeft => (timeLeft <= DEFAULT_TENSE_TIME_LEFT_IN_SECONDS))
			.Select(_ => {
				return true;
			})
			.ToReactiveProperty();

		reactiveIsOver = reactiveCountdown.Where(timeLeft => (timeLeft <= 0))
			.Select(time => {
				reactiveHasStarted.Value = false;
				return true;
		}).ToReactiveProperty();

		SetDefaultCountdown();
	}

	private void SetDefaultCountdown() {
		reactiveCountdown.Value = (PlayerPrefs.HasKey(PlayerPrefKey.TIME)) 
			? (PlayerPrefs.GetInt(PlayerPrefKey.TIME)) : DEFAULT_COUNTDOWN_TIMER_IN_SECONDS;
	}

	public void StartGameCountdown() {
		reactiveHasStarted.Value = true;
		LogUtil.PrintInfo(this.GetType(), "StartGameCountdown() starting countdown...");
	}

}
