using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	/// <summary>
	///three platform in the scene
	/// </summary>
	public GameObject startPlatform, abovePlatform, belowPlatform;
	/// <summary>
	///main character
	/// </summary>
	private GameObject theBird;
	/// <summary>
	///check the first jump
	/// </summary>
	public static bool firstJump;
	/// <summary>
	///specially point
	/// </summary>
	public Transform bottomMin, bottomMax, topMin, topMax, coinMin, coinMax, bottomHiden, topHiden;
	/// <summary>
	///total of coin
	/// </summary>
	public GameObject coin;
	/// <summary>
	///shake the bird when it died
	/// </summary>
	GameObject shake;
	/// <summary>
	///curren coind and current point
	/// </summary>
	[HideInInspector]
	public int currentCoin, currentPoint;
	/// <summary>
	///bird characters
	/// </summary>
	public GameObject[] birdPrefabs;
	/// <summary>
	///check if go to shop
	/// </summary>
	public bool inShop;
	/// <summary>
	///float text when the bird hit coin
	/// </summary>
	public GameObject floatText;
	//delegate
	public delegate void _GameStart ();

	public static event _GameStart OnGameStart;

	public delegate void _GameOver ();

	public static event _GameOver OnGameOver;

	public delegate void _UpdateCoin (int _coin);

	public static event _UpdateCoin OnUpdateCoin;

	public delegate void _UpdatePoint (int _point);

	public static event _UpdatePoint OnUpdatePoint;

	void Awake ()
	{
		firstJump = true;
		CreateBird ();
	}

	// Use this for initialization
	void Start ()
	{
		Random.seed = System.Environment.TickCount;
		theBird = GameObject.FindGameObjectWithTag ("Bird");
		inShop = false;
		currentCoin = Coin;
		currentPoint = 0;
		shake = GameObject.FindGameObjectWithTag ("Shake");
		shake.GetComponent<Animator> ().enabled = false;
		abovePlatform.name = "Above";
		belowPlatform.name = "Below";
	}
	/// <summary>
	///instance the bird that choose in shop
	/// </summary>
	void CreateBird ()
	{
		int birdType = PlayerPrefs.GetInt ("Bird");
		Instantiate (birdPrefabs [birdType],
			new Vector3 (0.0f, bottomMax.position.y, 0.0f),
			transform.rotation);
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
	/// <summary>
	///move smoothly object
	/// </summary>
	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i = 0.0f;
		var rate = 1.0f / time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null; 
		}
	}
	/// <summary>
	///move above platform
	/// </summary>
	IEnumerator MoveAbovePlatform (Transform thisTransform, Vector3 startPos, Vector3 endPos1,
	                              Vector3 endPos2,
	                              float time1, float time2)
	{
		var i = 0.0f;
		var rate = 1.0f / time1;

		while (i < 1.0f) {
			
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos1, i);
			yield return null; 

		}

		i = 0.0f;
		rate = 1.0f / time2;

		while (i < 1.0f) {
			
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (endPos1, endPos2, i);
			yield return null; 

		}
	}
	/// <summary>
	///move below platform if the bird can jump to higher platform
	/// </summary>
	IEnumerator MoveBelowPlatformGameOver (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i = 0.0f;
		var rate = 1.0f / time;
		while (i < 1.0f) {
			
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null; 

		}
	}
	/// <summary>
	///move below platform if the bird can jump to higher platform
	/// </summary>
	IEnumerator MoveBelowPlatformClear (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i = 0.0f;
		var rate = 1.0f / time;

		while (i < 1.0f) {
			
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, bottomHiden.position, i);
			yield return null; 

		}

		i = 0.0f;
		rate = 1.0f / time;

		while (i < 1.0f) {
			
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (topHiden.position, endPos, i);
			yield return null; 

		}

		GameObject temp = null;

		temp = belowPlatform;
		belowPlatform = abovePlatform;
		abovePlatform = temp;
		abovePlatform.name = "Above";
		belowPlatform.name = "Below";

		int coinRandom = Random.Range (0, 2);
		if (coinRandom == 1 && theBird.GetComponent<Bird> ().coinGetAlready) {
			
			theBird.GetComponent<Bird> ().coinGetAlready = false;
			float coinPos = Random.Range (coinMin.position.y, coinMax.position.y);
			Instantiate (coin, new Vector3 (0.0f, coinPos, 0.0f), transform.rotation);

		}
	}

	public void FirstSwapPlatform ()
	{
		LeaveStartPlatform ();
		theBird.transform.parent = belowPlatform.transform;
		UpdatePoint ();

		float targetPos1 = Random.Range (bottomMin.position.y, bottomMax.position.y);
		StartCoroutine (MoveAbovePlatform (belowPlatform.transform, belowPlatform.transform.position,
			new Vector3 (0.0f, belowPlatform.transform.position.y - 0.5f * (belowPlatform.transform.position.y - targetPos1), 0.0f),
			bottomHiden.position, 0.25f, 10.0f));
		
		float tartgetPos2 = Random.Range (topMin.position.y, topMax.position.y);
		StartCoroutine (MoveObject (abovePlatform.transform, abovePlatform.transform.position,
			new Vector3 (0.0f, tartgetPos2, 0.0f), 0.25f));
	}

	public void MoveBelowPlatformGameOver ()
	{
		StopAllCoroutines ();
		float targetPos1 = Random.Range (bottomMin.position.y, bottomMax.position.y);
		StartCoroutine (MoveBelowPlatformGameOver (belowPlatform.transform, belowPlatform.transform.position,
			bottomHiden.position, 0.25f));
	}

	public void MoveBelowPlatformClear ()
	{
		StopAllCoroutines ();
		UpdatePoint ();
		float tartgetPos2 = Random.Range (topMin.position.y, topMax.position.y);
		StartCoroutine (MoveBelowPlatformClear (belowPlatform.transform, belowPlatform.transform.position,
			new Vector3 (0.0f, tartgetPos2, 0.0f), 0.25f));
	}

	public void MoveAbovePlatformTop ()
	{
		float targetPos1 = Random.Range (bottomMin.position.y, bottomMax.position.y);
		StartCoroutine (MoveAbovePlatform (abovePlatform.transform, abovePlatform.transform.position,
			new Vector3 (0.0f, abovePlatform.transform.position.y - 0.5f * (abovePlatform.transform.position.y - targetPos1), 0.0f),
			bottomHiden.position, 0.25f, 10.0f));
	}

	public void LeaveStartPlatform ()
	{
		StartCoroutine (MoveObject (startPlatform.transform, startPlatform.transform.position, bottomHiden.position, 0.5f));
	}
	/// <summary>
	///shake platform and bird when bird die
	/// </summary>
	public void Shake ()
	{
		shake.GetComponent<Animator> ().enabled = true;
		startPlatform.transform.parent = shake.transform;
		theBird.transform.parent = shake.transform;
		abovePlatform.transform.parent = shake.transform;
		belowPlatform.transform.parent = shake.transform;
	}

	/// <summary>
	///game start
	/// </summary>
	public void OnStart ()
	{
		if (OnGameStart != null)
			OnGameStart ();
	}
	/// <summary>
	///show ads and UI when game is over
	/// </summary>
	public void GameOver ()
	{
		Debug.Log("mati");
		int showRandom = Random.Range (0, 2);
		//if (showRandom == 1)
		//	AdsControl.Instance.showAds ();
		Coin = currentCoin;
		BestPoint = currentPoint;
		if (OnGameOver != null)
			OnGameOver ();
	}
	/// <summary>
	///update UI when bird get coin
	/// </summary>
	public void UpdateCoin ()
	{
		currentCoin++;
		if (OnUpdateCoin != null)
			OnUpdateCoin (currentCoin);
	}
	/// <summary>
	///show float text when the bird get coin
	/// </summary>
	public void ShowFloatText (Vector3 pos)
	{
		floatText.transform.position = pos;
		floatText.GetComponent<Animator> ().SetBool ("Show", true);
	}
	/// <summary>
	///uodate oint when the bird get coin
	/// </summary>
	public void UpdatePoint ()
	{
		currentPoint++;
		if (OnUpdatePoint != null)
			OnUpdatePoint (currentPoint);
	}

	public int Coin {
		get {
			int coin = PlayerPrefs.GetInt ("Coin", 0);
			return coin;
		}

		set {
			PlayerPrefs.SetInt ("Coin", value);
			PlayerPrefs.Save ();
		}
	}

	public int BestPoint {
		get {
			int best = PlayerPrefs.GetInt ("Best", 0);

			return best;
		}

		set {
			int best = PlayerPrefs.GetInt ("Best", 0);
			if (value >= best) {
				PlayerPrefs.SetInt ("Best", value);
				PlayerPrefs.Save ();
			}
		}
	}
}
