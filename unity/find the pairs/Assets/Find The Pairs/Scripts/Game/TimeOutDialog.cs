using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
[DisallowMultipleComponent]
public class TimeOutDialog : MonoBehaviour
{
		/// <summary>
		/// TimeOut Dialog animator.
		/// </summary>
		public  Animator timeOutDialogAnimator;

		/// <summary>
		/// The timeout dialog image.
		/// </summary>
		public Image timeOutDialogSpriteImage;


		// Use this for initialization
		void Awake ()
		{
				///Setting up the references
				if (timeOutDialogAnimator == null) {
						timeOutDialogAnimator = GetComponent<Animator> ();
				}
		
				if (timeOutDialogSpriteImage == null) {
						timeOutDialogSpriteImage = GetComponent<Image> ();
				}
		}

		/// <summary>
		/// When the GameObject becomes visible
		/// </summary>
		void OnDisable ()
		{
				///Hide the TimeOut Dialog
				Hide ();
		}

		///Show the TimeOut Dialog
		public void Show ()
		{
				if (timeOutDialogAnimator == null) {
						return;
				}
				timeOutDialogSpriteImage.enabled = true;
				timeOutDialogAnimator.SetTrigger ("Running");
		}

		///Hide the TimeOut Dialog
		public void Hide ()
		{
				if (timeOutDialogAnimator != null)
						timeOutDialogAnimator.SetBool ("Running", false);
		}
}