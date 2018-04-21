﻿using UnityEngine;
using System.Collections;

public class MSPMoreActionsExample : MonoBehaviour {

	public Texture2D imageForPosting;

	void Awake() {
		SPInstagram.OnPostingCompleteAction += OnPostingCompleteAction;
	}

	public void InstaShare() {
		SPInstagram.Instance.Share(imageForPosting);
	}

	public void InstaShareWithText() {
		SPInstagram.Instance.Share(imageForPosting, "I am posting from my app");
	}

	public void NativeShare() {
		//SPShareUtility.VideoShare("video sharing");
		SPShareUtility.ShareMedia("Share Caption", "Share Message");
	}

	public void NativeShareWithImage() {
		SPShareUtility.ShareMedia("Share Caption", "Share Message", imageForPosting);
	}

	public void SendMail() {
		SPShareUtility.SendMail( "My E-mail Subject", "This is my text to share", "mail1@gmail.com, mail2@gmail.com", imageForPosting);
	}

	public void SendWhatsapp() {
		SPShareUtility.WhatsappShare ("This is my text to share", imageForPosting);
	}

	void OnPostingCompleteAction (InstagramPostResult result) {
		if(result == InstagramPostResult.RESULT_OK) {
			Debug.Log("Posting Successful");
		} else {
			Debug.Log("Posting failed with error code " + result.ToString());
		}
	}


}
