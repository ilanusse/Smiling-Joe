using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

	private float healthTimer;
	public float inSightHealthTime;

	// Use this for initialization
	void Start () {
		healthTimer = 0f;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void decreaseHealth() {
		healthTimer += Time.deltaTime;
		if(healthTimer >= inSightHealthTime) {
			die();
		}
	}

	private void die() {
		Application.LoadLevel ("menu");
	}
}
