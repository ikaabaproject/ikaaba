using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Example_02 : MonoBehaviour {

	private GameObject ALBUM;
	private GameObject CAR_MAIN;
	private bool isShowAlbum;
	private int counter;
	private List<GameObject> Album_holder = new List<GameObject>();
	private bool waitForSreenshot;

	void Start()
	{
		// Setup the scene and album
		waitForSreenshot = false;
		counter = 0;
		isShowAlbum = false;
		ALBUM = GameObject.Find("Camera/Main Camera/ALBUM");
		CAR_MAIN = GameObject.Find("SCENE/CAR_MAIN");

		for(int i=1;i<=6;i++)
		{
			Album_holder.Add(GameObject.Find("Camera/Main Camera/ALBUM/"+i.ToString()));
		}

		ALBUM.SetActive(false);
	}

	void OnGUI()
	{
		if(!waitForSreenshot)
		{
			if(!isShowAlbum)
			{
				// Make screenshot if album is turned off
				if(GUI.Button(new Rect(10,Screen.height-50,150,50),"Make Screenshot"))
				{
					waitForSreenshot = true;
					proScreenShot _proScreenShot = (proScreenShot) GameObject.Find("ScreenShotComponent").GetComponent<proScreenShot>();
					_proScreenShot.MakeScreenShot();

					// Add screenshot to album when it's made
					StartCoroutine(AddScreenShotToAlbum(++counter));
				}
			}
			else
			{
				GUI.Label(new Rect(Screen.width/2-40,10,100,30),"ALBUM");
			}

			// Show / hide album
			if(GUI.Button(new Rect(170,Screen.height-50,150,50),"ALBUM: "+isShowAlbum))
			{
				if(isShowAlbum)
				{
					HideAlbum();
				}
				else
				{
					ShowAlbum();
				}
			}
		}
	}

	public IEnumerator AddScreenShotToAlbum(int index)
	{
		yield return new WaitForSeconds(0.5f);

		if(counter>6)
		{
			counter = 1;
		}
		
		index = counter;

		proScreenShot _proScreenShot = (proScreenShot) GameObject.Find("ScreenShotComponent").GetComponent<proScreenShot>();
		Album_holder[index-1].GetComponent<MeshRenderer>().material.mainTexture = _proScreenShot.GetTextureFromPath(_proScreenShot.GetPathOfLastScreenShot());

		waitForSreenshot = false;

		yield return 0;
	}

	public void ShowAlbum()
	{
		isShowAlbum = true;
		CAR_MAIN.SetActive(false);
		ALBUM.SetActive(true);
	}

	public void HideAlbum()
	{
		isShowAlbum = false;
		ALBUM.SetActive(false);
		CAR_MAIN.SetActive(true);
	}
}
