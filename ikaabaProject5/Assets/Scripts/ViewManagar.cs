using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ViewManagar : MonoBehaviour {
    public GameObject[] viewAbleObject;
	public Image information;
	public Sprite[] infoSprite;
	public Image switchObjectImage;
	public Sprite[] objectList;
	public Canvas buttons;
    int activeID;
	// Use this for initialization
	void Start () {
		// disable 4 objects
        for (int i = 0; i < viewAbleObject.Length; i++)
        {
            viewAbleObject[i].SetActive(false);
            
        }
		//enable only first model
        viewAbleObject[activeID].SetActive(true);
    }
	
    public void Switch()
    {
		// change active model
        activeID++;
		// if active model is last change to first
        if(activeID > viewAbleObject.Length - 1)
            activeID = 0;
		//disable all
        for (int i=0;i<viewAbleObject.Length;i++)
        {
            viewAbleObject[i].SetActive(false);
        }
		//enable one object
        viewAbleObject[activeID].SetActive(true);
		//change information image
		information.sprite = infoSprite [activeID];
		switchObjectImage.sprite = objectList [activeID];
            
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}

	public void SaveScreenshot()
	{
		StartCoroutine (_Screenshot());
	}
	IEnumerator _Screenshot()
	{
		// wait one frame
		yield return null;
		// hide button
		buttons.enabled = false;
		yield return null;
		// save screenshot
		ScreenshotManager.SaveScreenshot ("KaabaScreenshot", "iKaaba", "jpeg");
		yield return null;
		buttons.enabled = true;
	}
}
