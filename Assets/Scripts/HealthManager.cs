using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

	private float healthTimer;
	public float inSightHealthTime;
	public VictoryManager victoryManager;
	public TipManager tipManager;


	// Use this for initialization
	void Start () {
		healthTimer = 0f;	
	}

	public void decreaseHealth() {
		healthTimer += Time.deltaTime;
		if(healthTimer >= inSightHealthTime) {
			die();
		}
	}

	private void die() {
		victoryManager.playerDied ();
	}
}
