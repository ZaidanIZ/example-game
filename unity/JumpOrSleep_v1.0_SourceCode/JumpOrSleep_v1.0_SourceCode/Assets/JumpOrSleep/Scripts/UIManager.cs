using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
	/// <summary>
	///UI panel
	/// </summary>
	public GameObject homeElement;
	public GameObject gameOverElement;
	public GameObject shopElement;
	/// <summary>
	///UI element that need updated in game
	/// </summary>
	public Text coinText;
	public Text pointText;
	public Text bestPoint;
	public Text pointResult;

	private GameManager gameManager;

	void OnEnable()
	{
		
		GameManager.OnGameStart += OnGameStart;
		GameManager.OnGameOver += OnGameOver;
		GameManager.OnUpdateCoin += OnUpdateCoin;
		GameManager.OnUpdatePoint += OnUpdatePoint;

	}

	void OnDisable()
	{
		
		GameManager.OnGameStart -= OnGameStart;
		GameManager.OnGameOver -= OnGameOver;
		GameManager.OnUpdateCoin -= OnUpdateCoin;
		GameManager.OnUpdatePoint -= OnUpdatePoint;

	}

	// Use this for initialization
	void Start () {
	
		gameManager = FindObjectOfType<GameManager> ();
		coinText.text = gameManager.Coin.ToString();
		bestPoint.text = gameManager.BestPoint.ToString ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnGameStart()
	{
		homeElement.SetActive (false);
	}

	public void OnGameOver()
	{
		pointResult.text = gameManager.currentPoint.ToString ();
		gameOverElement.GetComponent<Animator> ().SetBool ("GameOver", true);
	}

	void SetCoin(int coin)
	{
		coinText.text = coin.ToString ();
	}

	void SetPoint(int point)
	{
		pointText.text = point.ToString ();
	}

	void SetBestPoint()
	{
		bestPoint.text = gameManager.BestPoint.ToString();
	}

	void OnUpdateCoin(int _coin)
	{
		coinText.text = _coin.ToString ();
	}

	void OnUpdatePoint(int _point)
	{
		pointText.text = _point.ToString ();
	}

	public void RePlay()
	{
		Application.LoadLevel (0);
	}

	public void OpenShop()
	{
		gameManager.inShop = true;
		shopElement.SetActive (true);
	}
}
