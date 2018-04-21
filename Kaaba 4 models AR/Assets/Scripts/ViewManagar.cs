using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManagar : MonoBehaviour {
    public GameObject[] viewAbleObject;
	public Image information;
	public Sprite[] infoSprite;
	public Image switchObjectImage;
	public Canvas buttons;
    int activeID;
	// Use this for initialization
	void Start () {
		// disable 4 objects
        for (int i = 0; i < viewAbleObject.Length; i++)
        {
            viewAbleObject[i].SetActive(false);
            //viewAbleObject[i].transform.position = Vector3.zero;
        }
		//enable only first model
        viewAbleObject[activeID].SetActive(true);

        
    }

    void OnEnable()
    {
        ScreenshotManager.OnScreenshotTaken += ScreenshotTaken;
    }
    public void ToggleInfo()
    {
        information.enabled = !information.enabled;
    }
    public void Switch()
    {
		// change active model
        activeID++;
		// if active model is last, change to first
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
		// hide butto
		buttons.enabled = false;
		yield return null;
		// save screenshot
        
		ScreenshotManager.SaveScreenshot ("KaabaScreenshot", "iKaaba", "jpeg");
		// wait for frame
		yield return null;
		buttons.enabled = true;
	}
    void ScreenshotTaken(Texture2D image)
    {
        SPShareUtility.ShareMedia("Share Caption", "Share Screenshot", image);
    }
}
