using UnityEngine;
using System.Collections;

public class proCameraMovement : MonoBehaviour {

	private GameObject target;
	private GameObject cam;

	private void Start()
	{
		target = GameObject.Find("SkyCarBodyPaintwork");
		cam = GameObject.Find("Main Camera");
	}

	private void Update()
	{
		cam.transform.RotateAround(target.transform.position,Vector3.up, 20 * Time.deltaTime);
		cam.transform.LookAt(target.transform.position+new Vector3(0,0,0));
	}
}
