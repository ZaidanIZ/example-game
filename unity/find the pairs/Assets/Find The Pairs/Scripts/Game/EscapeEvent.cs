using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.

/// <summary>
/// Escape or Back event
/// </summary>

public class EscapeEvent : MonoBehaviour
{
		/// <summary>
		/// The name of the scene to be loaded.
		/// </summary>
		public string sceneName;

		/// <summary>
		/// Whether to leave the application on escape click.
		/// </summary>
		public bool leaveTheApplication;

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						OnEscapeClick ();
				}
		}

		/// <summary>
		/// On Escape click event.
		/// </summary>
		public void OnEscapeClick ()
		{
				if (leaveTheApplication) {
					GameObject exitConfirmDialog = GameObject.Find ("ExitConfirmDialog");
					if(exitConfirmDialog!=null){
						exitConfirmDialog.GetComponent<ConfirmDialog> ().Show ();
					}
				} else {
						StartCoroutine ("LoadSceneAsync");
				}
		}

		/// <summary>
		/// Loads the scene Async.
		/// </summary>
		IEnumerator LoadSceneAsync ()
		{
			if (!string.IsNullOrEmpty (sceneName)) {
				#if UNITY_PRO_LICENSE
					AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
					while (!async.isDone) {
						yield return 0;
					}
				#else
					SceneManager.LoadScene (sceneName);
					yield return 0;
				#endif
			}
		}
}