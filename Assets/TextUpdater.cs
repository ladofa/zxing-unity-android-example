using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUpdater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject j = GameObject.Find("Player");
		CameraController cameraController = j.GetComponent<CameraController>();
		cameraController.ScanResultUpdated.AddListener(updateText);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void updateText(string str)
	{
		GetComponent<UnityEngine.UI.Text>().text = str;
	}
}
