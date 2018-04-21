#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;

public class MSPPostProcess  {

	private const string BUNLDE_KEY = "SA_PP_BUNLDE_KEY";

	[PostProcessBuild(48)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {

		#if UNITY_IPHONE
		
		SA.IOSDeploy.ISD_Settings.Instance.AddFramework(SA.IOSDeploy.iOSFramework.Accounts);
		SA.IOSDeploy.ISD_Settings.Instance.AddFramework(SA.IOSDeploy.iOSFramework.Social);
		SA.IOSDeploy.ISD_Settings.Instance.AddFramework(SA.IOSDeploy.iOSFramework.MessageUI);


		SA.IOSDeploy.Variable LSApplicationQueriesSchemes =  new SA.IOSDeploy.Variable();
		LSApplicationQueriesSchemes = new SA.IOSDeploy.Variable();
		LSApplicationQueriesSchemes.Name = "LSApplicationQueriesSchemes";
		LSApplicationQueriesSchemes.Type = SA.IOSDeploy.PlistValueTypes.Array;
			

		SA.IOSDeploy.Variable instagram =  new SA.IOSDeploy.Variable();
		instagram.StringValue = "instagram";
		instagram.Type = SA.IOSDeploy.PlistValueTypes.String;
		LSApplicationQueriesSchemes.AddChild(instagram);

		SA.IOSDeploy.Variable whatsapp =  new SA.IOSDeploy.Variable();
		whatsapp.StringValue = "whatsapp";
		whatsapp.Type = SA.IOSDeploy.PlistValueTypes.String;
		LSApplicationQueriesSchemes.AddChild(whatsapp);

		SA.IOSDeploy.Variable fbauth2 =  new SA.IOSDeploy.Variable();
		fbauth2.StringValue = "fbauth2";
		fbauth2.Type = SA.IOSDeploy.PlistValueTypes.String;
		LSApplicationQueriesSchemes.AddChild(fbauth2);


		SA.IOSDeploy.ISD_Settings.Instance.AddVariable (LSApplicationQueriesSchemes);



		var NSCameraUsageDescription =  new SA.IOSDeploy.Variable();
		NSCameraUsageDescription.Name = "NSCameraUsageDescription";
		NSCameraUsageDescription.StringValue = "For Social Sharing";
		NSCameraUsageDescription.Type = SA.IOSDeploy.PlistValueTypes.String;


		SA.IOSDeploy.ISD_Settings.Instance.AddVariable(NSCameraUsageDescription);



		var NSPhotoLibraryUsageDescription =  new SA.IOSDeploy.Variable();
		NSPhotoLibraryUsageDescription.Name = "NSPhotoLibraryUsageDescription";
		NSPhotoLibraryUsageDescription.StringValue = "For Social Sharing";
		NSPhotoLibraryUsageDescription.Type = SA.IOSDeploy.PlistValueTypes.String;


		SA.IOSDeploy.ISD_Settings.Instance.AddVariable(NSPhotoLibraryUsageDescription);



		#endif
	}

}
#endif
