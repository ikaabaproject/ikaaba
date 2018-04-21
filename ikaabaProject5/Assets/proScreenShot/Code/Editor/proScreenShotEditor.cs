using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(proScreenShot))]
public class proScreenShotEditor : Editor {

	public void Awake()
	{
		proScreenShot myTarget = (proScreenShot)target;
		myTarget.textureFormats = new string[3];
		myTarget.textureFormats[0] = "ARGB32";
		myTarget.textureFormats[1] = "RGB24";
		myTarget.textureFormats[2] = "RGB16";

		myTarget.encodeFormats = new string[2];
		myTarget.encodeFormats[0] = "JPG";
		myTarget.encodeFormats[1] = "PNG";

		myTarget.blendFormats = new string[2];
		myTarget.blendFormats[0] = "Transparent";
		myTarget.blendFormats[1] = "Simple";
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();	

		proScreenShot myTarget = (proScreenShot)target;
		EditorGUILayout.LabelField("Screenshot Format");
		myTarget.textureFormatsIndex = EditorGUILayout.Popup(myTarget.textureFormatsIndex, myTarget.textureFormats);
		EditorGUILayout.LabelField("Encode Format");
		myTarget.encodeFormatsIndex = EditorGUILayout.Popup(myTarget.encodeFormatsIndex, myTarget.encodeFormats);
		EditorGUILayout.LabelField("Blend Type");
		myTarget.blendFormatsIndex = EditorGUILayout.Popup(myTarget.blendFormatsIndex, myTarget.blendFormats);

		if(GUILayout.Button("MAKE SCREENSHOT"))
		{
			myTarget.MakeScreenShot();
		}

		if(proCore.lastScreenShotTexture)
		{
			EditorGUILayout.LabelField("Screenshot Preview");
			GUILayout.Label(proCore.lastScreenShotTexture);

			if(GUILayout.Button("Delete"))
			{
				myTarget.DeleteLastScreenShot();
			}
		}
	}
}
