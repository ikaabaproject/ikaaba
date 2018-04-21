using UnityEngine;
using System.Collections;

public class Example_01 : MonoBehaviour {

	private GameObject cube;

	void Start()
	{
		cube = GameObject.Find("centercube");
	}
	
	void Update()
	{
		// Rotate camera
		Camera.main.gameObject.transform.RotateAround(cube.transform.position,Vector3.up, 20 * Time.deltaTime);
	}
	
	void OnGUI()
	{
		// Just make a simple screenshot
		if(GUI.Button(new Rect(10,10,250,70),"Make Screenshot"))
		{
			proScreenShot _proScreenShot = (proScreenShot) GameObject.Find("ScreenShotComponent").GetComponent<proScreenShot>();
			_proScreenShot.MakeScreenShot();
		}

		if(GUI.Button(new Rect(10,90,250,70),"Set Screenshot to Cube"))
		{
			proScreenShot _proScreenShot = (proScreenShot) GameObject.Find("ScreenShotComponent").GetComponent<proScreenShot>();

			// Get latest screenshot and assign it to the cube
			cube.GetComponent<MeshRenderer>().material.mainTexture = _proScreenShot.GetTextureFromPath(_proScreenShot.GetPathOfLastScreenShot());
		}
	}
}
