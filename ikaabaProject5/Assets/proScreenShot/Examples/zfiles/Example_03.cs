using UnityEngine;
using System.Collections;

public class Example_03 : MonoBehaviour {

	private Texture2D lastScreenShot = null;

	void OnGUI()
	{
		// Draw screenshot if available
		if(lastScreenShot==null)
		{
			if(GUI.Button(new Rect(10,Screen.height-50,150,50),"Make Screenshot"))
			{
				StartCoroutine(MakeScreenShot());
				
			}
		}
		else
		{
			GUI.Box(new Rect(0,0,Screen.width,Screen.height),"SCREEN SHOT");
			// Draw screenshot
			GUI.DrawTexture(new Rect(20,20,Screen.width-40,Screen.height-40),lastScreenShot);

			if(GUI.Button(new Rect(Screen.width/2-50,Screen.height-100,100,50),"DISCARD"))
			{
				// Discard the window and delete last screenshot
				proScreenShot _proScreenShot = (proScreenShot) GameObject.Find("ScreenShotComponent").GetComponent<proScreenShot>();
				_proScreenShot.DeleteLastScreenShot();
				lastScreenShot = null;
			}
		}
	}

	public IEnumerator MakeScreenShot()
	{
		// Make screenshot with settings in the Component
		proScreenShot _proScreenShot = (proScreenShot) GameObject.Find("ScreenShotComponent").GetComponent<proScreenShot>();
		_proScreenShot.MakeScreenShot();

		yield return new WaitForSeconds(0.5f);

		// Get last screenshot
		lastScreenShot = _proScreenShot.GetTextureFromPath(_proScreenShot.GetPathOfLastScreenShot());

		yield return 0;
	}
}
