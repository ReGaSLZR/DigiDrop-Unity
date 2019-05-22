using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCatcher : MonoBehaviour {

	public GameManager gameManager;
	public GameObject caughtEffect;

	void Start()
	{
		if(gameManager == null) 
		{
			LogUtil.PrintError(gameObject, GetType(), "NO GameManager set!");
			Destroy(this);
		}
	}

	void OnTriggerEnter(Collider collider) 
	{
		if(collider.tag.Equals(Tag.COIN)) 
		{
			if(caughtEffect != null) {
				Instantiate(caughtEffect, collider.gameObject.transform.position, Random.rotation);
			}
			gameManager.StoreCoinCaught();
			Destroy(collider.gameObject);
		}
		else 
		{
			LogUtil.PrintInfo(gameObject, GetType(), "Caught a different object with tag = " + collider.tag);
		}
	}

}
