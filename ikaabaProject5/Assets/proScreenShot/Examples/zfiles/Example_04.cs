using UnityEngine;
using System.Collections;

public class Example_04 : MonoBehaviour {

	private Texture2D lastScreenShot;
	private proScreenShot _proScreenshot;

	private void Start()
	{
		_proScreenshot = this.gameObject.GetComponent<proScreenShot>();
	} 

	private void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,200,50),"Make Screenshot"))
		{
			// Make Screenshot
			StartCoroutine(MakeScreenShot());
		}

		if(GUI.Button(new Rect(10,60,200,50),"Delete Last Screenshot"))
		{
			// Delete last screenshot
			_proScreenshot.DeleteLastScreenShot();
			lastScreenShot = null;
		}

		if(GUI.Button(new Rect(10,110,200,50),"Delete All Screenshots"))
		{
			// Delete all screenshots (Documents and Editor)
			_proScreenshot.DeleteAllScreenShots();
			lastScreenShot = null;
		}

		if(lastScreenShot!=null)
		{
			// Draw last screenshot if available
			GUI.DrawTexture(new Rect(Screen.width-170,10,160,120),lastScreenShot);
		}
	}

	private IEnumerator MakeScreenShot()
	{
		// Set Custom Name
		_proScreenshot.SetName("CustomName");

		// Set Extension
		_proScreenshot.SetExtension(".png");

		// Load Texture From Resources and Blend it with screenshot
		_proScreenshot.AddBlendTexture(new Vector2(0,-250),(Texture2D)Resources.Load("watermark"));

		// Ignore Red Cube in the scene
		_proScreenshot.AddIgnoreObject(GameObject.Find("RedCube"));

		// Set Custom resolution for screenshot and disable to use screen resolution
		_proScreenshot._useScreenResolution = false;
		_proScreenshot.SetShotResolution(800,600);

		// Set blend format
		_proScreenshot.SetBlendFormat("Transparent");

		// Set Encode format
		_proScreenshot.SetEncodeFormat("JPG");

		// Set Texture format
		_proScreenshot.SetTextureFormat("ARGB32");

		// Set Sound
		_proScreenshot._screenshotSound = (AudioClip) Resources.Load("sound");

		// Add date to screenshot name
		_proScreenshot._addDateFileName = true;

		// Add Random Number to screenshot name
		_proScreenshot._addRandomNumberToName = true;

		// Render UI
		_proScreenshot.IsRenderUI(false);

		// Make Screenshot
		_proScreenshot.MakeScreenShot();

		// Wait for screenshot to make
		yield return new WaitForSeconds(0.3f);

		// Get Latest screenshot and draw it
		lastScreenShot = _proScreenshot.GetTextureFromPath(_proScreenshot.GetPathOfLastScreenShot());
		yield return 0;
	}


}
