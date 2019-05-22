using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class BGMPresenter : MonoBehaviour {

	[SerializeField] private AudioClip[] bgmClips;
	[SerializeField] private AudioSource bgmAudioSource;

	[Inject]
	private CountdownTimerModel model;

	private void Awake() {
		if(bgmClips == null || bgmClips.Length == 0) {
			LogUtil.PrintWarning(this, GetType(), "No bgmClips set!");
			return;
		}

		bgmAudioSource.clip = bgmClips[Random.Range(0, bgmClips.Length)];
	}

	private void Start() {
		InitializePresenter();
	}

	private void InitializePresenter() {
		model.reactiveIsOver
			.Where(isOver => (isOver == true))
			.Subscribe(_ => {
				StopBGM();
			})
			.AddTo(this);

		model.reactiveHasStarted
			.Where(hasStarted => (hasStarted == true))
			.Subscribe(_ => {
				PlayBGM();
			})
			.AddTo(this);
	}

	public void PlayBGM() {
		if(bgmClips == null || bgmClips.Length == 0) {
			LogUtil.PrintWarning(this, GetType(), "No bgmClips set!");
			return;
		}

		bgmAudioSource.Play();
	}

	public void StopBGM() {
		bgmAudioSource.Stop();
	}

}
