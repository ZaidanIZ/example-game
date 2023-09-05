#pragma warning disable 414

using UnityEngine;
using System.Collections;
using System;
using System.IO;

namespace TrickshotArena
{
	public class SharingSystem : MonoBehaviour
	{
		public AudioClip cameraSfx;                         //screenshot sfx
		private bool canTap;                                //flag to prevent double share
		public static bool canCapture;

		//shot settings
		private string gameTitle = "YOUR GAME NAME HERE";   //Update this with your own game name
		private string path;
		private string imageName;
		private string fullPath;

		void Start()
		{
			canTap = true;
			canCapture = true;
		}

		void Update()
		{
			if (canTap)
				StartCoroutine(touchManager());
		}

		IEnumerator captureScreenshot()
		{
			print("Capturing a new shot!... | Level: #" + GlobalGameManager.level.ToString());
			canCapture = false;

			//we need to wait for the units to appear!
			yield return new WaitForSeconds(1.0f);

			path = Application.persistentDataPath;
			imageName = "gameshot.png";
			fullPath = path + "/" + imageName;

#if UNITY_ANDROID
		ScreenCapture.CaptureScreenshot (imageName);
#endif

#if UNITY_IOS
		Application.CaptureScreenshot (imageName);
#endif

#if UNITY_EDITOR
			ScreenCapture.CaptureScreenshot(fullPath);
#endif

			yield return new WaitForSeconds(1.5f); //make sure our image has been saved.
			print("Save Completed!!");
			print(fullPath);
		}


		/// <summary>
		/// Detect touch on CaptureShot (Share) button
		/// </summary>
		private RaycastHit hitInfo;
		private Ray ray;
		IEnumerator touchManager()
		{
			//Mouse of touch?
			if (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Ended)
				ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			else if (Input.GetMouseButtonUp(0))
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			else
				yield break;

			if (Physics.Raycast(ray, out hitInfo))
			{
				GameObject objectHit = hitInfo.transform.gameObject;
				switch (objectHit.name)
				{
					case "Button-Share":
						canTap = false;
						StartCoroutine(reactiveTap());
						playSfx(cameraSfx);
						StartCoroutine(captureScreenshot());
						//#if UNITY_ANDROID && !UNITY_EDITOR
						ShareImage(fullPath, gameTitle, gameTitle, "OMG! I scored " + GlobalGameManager.level + " in #" + gameTitle + ". Can you beat my score? " + "https://play.google.com/store/apps/details?id=com.finalboss.infinitefootball");
						//#endif
						break;
				}
			}
		}


		/// <summary>
		/// Shares the captured image with android Intents.
		/// </summary>
		public static void ShareImage(string imageFileName, string subject, string title, string message)
		{
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("setType", "image/*");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), title);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);

			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", imageFileName);
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);

			bool fileExist = fileObject.Call<bool>("exists");
			Debug.Log("File exist : " + fileExist);
			// Attach image to intent
			if (fileExist)
				intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			currentActivity.Call("startActivity", intentObject);
		}


		/// <summary>
		/// Play sound clips
		/// </summary>
		/// <param name="_clip"></param>
		void playSfx(AudioClip _clip)
		{
			GetComponent<AudioSource>().clip = _clip;
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}

		/// <summary>
		/// Reactives the touch system.
		/// </summary>
		IEnumerator reactiveTap()
		{
			yield return new WaitForSeconds(2.0f);
			canTap = true;
		}
	}
}