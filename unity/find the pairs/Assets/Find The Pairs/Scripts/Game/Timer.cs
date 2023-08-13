using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
[DisallowMultipleComponent]
public class Timer : MonoBehaviour
{
		/// <summary>
		/// Text Component
		/// </summary>
		public Text uiText;

		/// <summary>
		/// The time in seconds.
		/// </summary>
		public int timeInSeconds = 60;

		/// <summary>
		/// The temp time.
		/// </summary>
		private int tempTime;

		/// <summary>
		/// Whether the Timer is running
		/// </summary>
		private bool isRunning;

		/// <summary>
		/// Wheter to start the timer automatically.
		/// </summary>
		public bool autoStart;

		/// <summary>
		/// The time out game object.
		/// </summary>
		public GameObject timeOutGameObject;

		/// <summary>
		/// The time out call back.
		/// </summary>
		public string timeOutCallBack;

		/// <summary>
		/// The time prefix.
		/// </summary>
		public string prefix = "Time : ";

		void Awake ()
		{
				if (uiText == null) {
						uiText = GetComponent<Text> ();
				}
				
				if (autoStart) {
						///Start the Timer
						StartTimer ();
				}
		}

		/// <summary>
		/// Start the Timer.
		/// </summary>
		public void StartTimer ()
		{
				Reset ();
				if (!isRunning) {
						isRunning = true;
						StartCoroutine ("Wait");
				}
		}

		/// <summary>
		/// Stop the Timer.
		/// </summary>
		public void Stop ()
		{
				if (isRunning) {
						isRunning = false;
						StopCoroutine ("Wait");
				}
		}

		public void Reset ()
		{
				tempTime = timeInSeconds;
		}

		/// <summary>
		/// Wait Coroutine.
		/// </summary>
		private IEnumerator Wait ()
		{
				while (isRunning) {
						tempTime--;
						ApplyTime ();
						if (tempTime == 0) {
								Stop ();
								if (timeOutGameObject != null && !string.IsNullOrEmpty (timeOutCallBack)) {
										timeOutGameObject.SendMessage (timeOutCallBack);//Fire the timeout callback
								}
						}
						yield return new WaitForSeconds (1);
				}
		}

		/// <summary>
		/// Applies the time into TextMesh Component.
		/// </summary>
		private void ApplyTime ()
		{
				if (uiText == null) {
						return;
				}
				uiText.text = prefix + tempTime;
		}

		/// <summary>
		/// Sets the time.
		/// </summary>
		/// <param name="time">Time.</param>
		public void SetTime(int time){
			timeInSeconds = tempTime = time;
			ApplyTime ();
		}

		/// <summary>
		/// Get the time in seconds.
		/// </summary>
		public int GetTimeInSeconds(){
			return tempTime;
		}
}