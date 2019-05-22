using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeLimiter : MonoBehaviour {

	[SerializeField] private float lifetime = 3f;

	private void Awake () {
		Destroy(gameObject, lifetime);
	}

}
