using UnityEngine;

public class CoinDestroyer : MonoBehaviour {

	public GameManager gameManager;

	void OnTriggerEnter(Collider collider) 
	{
		if(collider.tag.Equals(Tag.COIN)) 
		{
			gameManager.StoreCoinUNCaught();
		}
		else 
		{
			LogUtil.PrintInfo(gameObject, GetType(), "detected object. TAG: " 
				+ collider.gameObject.tag + " | NAME: " + collider.gameObject.name);
		}

		Destroy(collider.gameObject);
	}

}
