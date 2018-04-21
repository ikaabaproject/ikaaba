using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EveryplayRecorder : MonoBehaviour {
	public Image button;
	// Use this for initialization
	void Start () {
		Everyplay.Initialize ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartRecording()
	{
		if (Everyplay.IsRecording ()) {
			button.color = Color.green;
			Everyplay.StopRecording ();
			Everyplay.PlayLastRecording ();
		} else {
			button.color = Color.red;
			Everyplay.StartRecording ();
		}
	}

	public void StopRecording()
	{
		Everyplay.StopRecording ();
	}

	public void ShareRecording()
	{
		Everyplay.ShowSharingModal ();
	}
}
