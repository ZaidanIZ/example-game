using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopManager : MonoBehaviour {
	/// <summary>
	///Bird shop element
	/// </summary>
	public Sprite[] birdIcon;
	public Image[] birdButton;
	public int[] birdPrice;
	// Use this for initialization
	void Start () {
		
		PlayerPrefs.SetInt ("Bird0", 1);
		GetData ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/// <summary>
	///buy bird or choose bird when click the button
	/// </summary>
	public void BuyBird(int type)
	{
		
		int birdLock = PlayerPrefs.GetInt ("Bird" + type.ToString ());
		//if bird is lock, buy bird else choose bird
		if (birdLock == 0) {
			int currentCoin = PlayerPrefs.GetInt ("Coin", 0);
			if (currentCoin >= birdPrice [type]) {
				
				currentCoin -= birdPrice [type];
				PlayerPrefs.SetInt ("Bird" + type.ToString (), 1);
				birdButton [type].sprite = birdIcon [type];
				birdButton [type].gameObject.transform.Find ("Price").gameObject.SetActive (false);
				birdButton [type].gameObject.transform.Find ("icon").gameObject.SetActive (false);

			}
		} else {
			
			PlayerPrefs.SetInt ("Bird", type);
			Application.LoadLevel (0);

		}
	}
	/// <summary>
	///show shop
	/// </summary>
	void GetData()
	{
		//birdButton [0].sprite = birdIcon [0];
		for (int i = 0; i < birdPrice.Length; i++) {
			int birdLock = PlayerPrefs.GetInt ("Bird" + i.ToString ());
			if (birdLock > 0) {
				birdButton [i].sprite = birdIcon [i];
				birdButton [i].gameObject.transform.Find ("Price").gameObject.SetActive (false);
				birdButton [i].gameObject.transform.Find ("icon").gameObject.SetActive (false);
			}
		}
	}
}
