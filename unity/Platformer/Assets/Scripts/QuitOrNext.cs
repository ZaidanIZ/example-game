using UnityEngine;
using UnityEngine.SceneManagement;
public class QuitOrNext : MonoBehaviour
{
	public string NameScene = "Level2";
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.tag);
		if (collision.tag.Equals("Player"))
		{
			SceneManager.LoadScene(NameScene);

		}
	}
}