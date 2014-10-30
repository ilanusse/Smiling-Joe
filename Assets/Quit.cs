using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	public KeyCode escape;
	// Update is called once per frame
	void Update () {
		Screen.showCursor = false;
		if (Input.GetKey (escape)) {
			Application.LoadLevel("menu");
		}
	}
}
