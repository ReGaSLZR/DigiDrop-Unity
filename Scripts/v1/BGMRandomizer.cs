using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMRandomizer : MonoBehaviour {

	public AudioClip[] _bgmClips;
	public AudioSource _audioSource;

	private void Awake() {
		if(_bgmClips == null || _bgmClips.Length == 0) {
			LogUtil.PrintError(gameObject, GetType(), "NO bgmClips set!");
		}
	}

	private void Start() {
		int randomClipIndex = Random.Range(0, _bgmClips.Length);
		_audioSource.clip = _bgmClips[randomClipIndex];
	}

	public void PlayRandomBGM() {
		if(_bgmClips == null || _bgmClips.Length == 0) { 
			LogUtil.PrintError(gameObject, GetType(), "NO bgmClips set!");
			return;
		}

		_audioSource.Play();
	}

	public void StopBGM() {
		_audioSource.Stop();
	}

}
