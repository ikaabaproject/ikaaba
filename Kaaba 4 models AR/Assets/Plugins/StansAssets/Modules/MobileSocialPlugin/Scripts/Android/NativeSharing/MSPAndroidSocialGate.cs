using UnityEngine;
using System;
using System.Text;
using System.Collections;

public class MSPAndroidSocialGate  {

	public static void StartGooglePlusShare(string text, Texture2D texture = null) {
		AN_SocialSharingProxy.StartGooglePlusShareIntent(text, texture == null ? string.Empty : System.Convert.ToBase64String(texture.EncodeToPNG()));
	}

	public static void ShareTwitterGif(string path, string message) {
		AN_SocialSharingProxy.ShareTwitterGif (path, message);
	}

	public static void StartShareIntent(string caption, string message, string packageNamePattern = "") {
		StartShareIntentWithSubject(caption, message, "", packageNamePattern);
	}
	
	public static void StartShareIntentWithSubject(string caption, string message, string subject, string packageNamePattern = "") {
		AN_SocialSharingProxy.StartShareIntent(caption, message, subject, packageNamePattern);
	}	
	
	public static void StartShareIntent(string caption, string message, Texture2D texture,  string packageNamePattern = "") {
		StartShareIntentWithSubject(caption, message, "", texture, packageNamePattern);
	}

	public static void StartShareIntent(string caption, string message, Texture2D[] textures, string packageNamePattern = "") {
		StartShareIntentWithSubject(caption, message, "", textures, packageNamePattern);
	}

	public static void StartShareIntentWithSubject(string caption, string message, string subject, Texture2D texture,  string packageNamePattern = "") {
		if(texture != null) {
			byte[] val = texture.EncodeToPNG();
			string bytesString = System.Convert.ToBase64String (val);			
			AN_SocialSharingProxy.StartShareIntent(caption, message, subject, bytesString, packageNamePattern, 1, SocialPlatfromSettings.Instance.SaveImageToGallery);
		} else {
			AN_SocialSharingProxy.StartShareIntent(caption, message, subject, packageNamePattern);
		}
	}

	public static void StartShareIntentWithSubject(string caption, string message, string subject, Texture2D[] textures, string packageNamePattern = "") {
		if (textures == null) {
			StartShareIntentWithSubject(caption, message, subject, packageNamePattern);
		} else if (textures.Length == 0) {
			StartShareIntentWithSubject(caption, message, subject, packageNamePattern);
		} else if (textures.Length == 1) {
			StartShareIntent(caption, message, textures[0], packageNamePattern);
		} else {
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < textures.Length - 1; i++) {
				builder.Append(Convert.ToBase64String(textures[i].EncodeToPNG()));
				builder.Append(AndroidNative.DATA_SPLITTER);
			}
			builder.Append(Convert.ToBase64String(textures[textures.Length - 1].EncodeToPNG()));

			AN_SocialSharingProxy.StartShareCollectionIntent(caption,
												message,
												subject,
												builder.ToString(),
												packageNamePattern,
												1,
												SocialPlatfromSettings.Instance.SaveImageToGallery);
		}
	}

	public static void SendMail(string caption, string message, string subject, string recipients, Texture2D[] textures) {
		if (textures == null) {
			SendMail(caption, message, subject, recipients);
		} else if (textures.Length == 0) {
			SendMail(caption, message, subject, recipients);
		} else if (textures.Length == 1) {
			SendMail(caption, message, subject, recipients, textures[0]);
		} else {
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < textures.Length - 1; i++) {
				builder.Append(Convert.ToBase64String(textures[i].EncodeToPNG()));
				builder.Append(AndroidNative.DATA_SPLITTER);
			}
			builder.Append(Convert.ToBase64String(textures[textures.Length - 1].EncodeToPNG()));

			AN_SocialSharingProxy.SendMailWithImages(caption,
												message,
												subject,
												recipients,
												builder.ToString(),
												1,
												SocialPlatfromSettings.Instance.SaveImageToGallery);
		}
	}

	public static void SendMail(string caption, string message,  string subject, string recipients, Texture2D texture = null) {
		if(texture != null) {
			byte[] val = texture.EncodeToPNG();
			string mdeia = System.Convert.ToBase64String (val);
			AN_SocialSharingProxy.SendMailWithImage(caption, message, subject, recipients, mdeia, 1, SocialPlatfromSettings.Instance.SaveImageToGallery);
		} else {
			AN_SocialSharingProxy.SendMail(caption, message, subject, recipients);
		}		
	}

	public static void StartVideoShareIntent(string message, string caption) {
		AndroidNative.GetVideoAndStartShareIntent(message, caption);
	}
}

