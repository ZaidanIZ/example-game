using UnityEngine;
using UnityEngine.UI;

public class ShowScore: MonoBehaviour {

	public Text txScore;

	// Update is called once per frame
	void FixedUpdate()
	{
		GetComponent<Text>().text = Data.score.ToString();
	}
}