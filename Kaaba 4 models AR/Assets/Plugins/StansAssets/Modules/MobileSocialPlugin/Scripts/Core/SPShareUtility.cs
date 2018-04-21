using UnityEngine;
using System.Collections;

public class SPShareUtility  {

	public static void TwitterShare(string status) {
		TwitterShare(status, null);
	}

	public static void VideoShare(string message) {
		switch (Application.platform) {
			case RuntimePlatform.Android:
				MSPAndroidSocialGate.StartVideoShareIntent(message, "Select Video");
				break;
			case RuntimePlatform.IPhonePlayer:
				//TODO: Add iOS implementation here
				break;
		}
	}
	
	public static void TwitterShare(string status, Texture2D texture) {
		switch(Application.platform) {
		case RuntimePlatform.Android:
			MSPAndroidSocialGate.StartShareIntent("Share", status, texture, "twi");
			break;
		case RuntimePlatform.IPhonePlayer:
			MSPIOSSocialManager.Instance.TwitterPost(status, null, texture);
			break;
		}
	}

	public static void TwitterGifShare(string message, string gifPath) {
		switch (Application.platform) {
		case RuntimePlatform.Android:
			MSPAndroidSocialGate.ShareTwitterGif (gifPath, message);
			break;
		case RuntimePlatform.IPhonePlayer:
			MSPIOSSocialManager.Instance.TwitterPostGif (message, gifPath);
			break;
		}
	}

	public static void FacebookShare(string message) {
		FacebookShare(message, null);
	}
	
	public static void FacebookShare(string message, Texture2D texture) {
		switch(Application.platform) {
		case RuntimePlatform.Android:
			MSPAndroidSocialGate.StartShareIntent("Share", message, texture, "facebook.katana");
			break;
		case RuntimePlatform.IPhonePlayer:
			MSPIOSSocialManager.Instance.FacebookPost(message, null, texture);
			break;
		}
	}


	public static void WhatsappShare(string message, Texture2D texture = null) {
		switch (Application.platform) {
		case RuntimePlatform.Android:
			MSPAndroidSocialGate.StartShareIntent ("", message, texture);
			break;
		case RuntimePlatform.IPhonePlayer:
			if (texture == null) {
				MSPIOSSocialManager.Instance.WhatsAppShareText (message);
			} else {
				MSPIOSSocialManager.Instance.WhatsAppShareImage (texture);
			}
			break;
		}
	}

	public static void ShareMedia(string caption, string message) {
		switch (Application.platform) {
			case RuntimePlatform.Android:
			#if UNITY_2017
				MSPAndroidSocialGate.StartShareIntent(caption, message, null);
			#else
				MSPAndroidSocialGate.StartShareIntent(caption, message, "");
			#endif
				break;
			case RuntimePlatform.IPhonePlayer:
				MSPIOSSocialManager.Instance.ShareMedia(message, null);
				break;
		}
	}
	
	public static void ShareMedia(string caption, string message, Texture2D texture) {
		switch(Application.platform) {
		case RuntimePlatform.Android:
			MSPAndroidSocialGate.StartShareIntent(caption, message, texture);
			break;
		case RuntimePlatform.IPhonePlayer:
			MSPIOSSocialManager.Instance.ShareMedia(message, texture);
			break;
		}
	}

	public static void ShareMedia(string caption, string message, Texture2D[] textures) {
		switch (Application.platform) {
			case RuntimePlatform.Android:
				MSPAndroidSocialGate.StartShareIntent(caption, message, textures);
				break;
			case RuntimePlatform.IPhonePlayer:
				//TODO: Add iOS platform implementation here
				break;
		}
	}
	
	public static void SendMail(string subject, string body, string recipients) {
		switch (Application.platform) {
			case RuntimePlatform.Android:
				MSPAndroidSocialGate.SendMail("Send Mail", body, subject, recipients);
				break;
			case RuntimePlatform.IPhonePlayer:
				MSPIOSSocialManager.Instance.SendMail(subject, body, recipients, null);
				break;
		}
	}
	
	public static void SendMail(string subject, string body, string recipients, Texture2D texture) {
		switch(Application.platform) {
		case RuntimePlatform.Android:
			MSPAndroidSocialGate.SendMail("Send Mail", body, subject, recipients, texture);
			break;
		case RuntimePlatform.IPhonePlayer:
			MSPIOSSocialManager.Instance.SendMail(subject, body, recipients, texture);
			break;
		}
	}

	public static void SendMail(string subject, string body, string recipients, Texture2D[] textures) {
		switch (Application.platform) {
			case RuntimePlatform.Android:
				MSPAndroidSocialGate.SendMail("Send Mail", body, subject, recipients, textures);
				break;
			case RuntimePlatform.IPhonePlayer:
				//TODO: Add iOS platform implementation here
				break;
		}
	}

}
