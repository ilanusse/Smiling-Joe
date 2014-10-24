using UnityEngine;
using System.Collections;

public class VictoryManager : MonoBehaviour
{
	public GameObject woman;
	public GameObject videoTape;
	public GameObject car;
	public KeyCode actionKey;
	public float minSqrDistance;

	private bool videoTapeCollected;
	private bool womanFound;
	
	void Start () {
		womanFound = false;
		videoTapeCollected = false;
	}

	void FixedUpdate () {
		womanSearch ();
		videoTapeSearch();
		carSearch ();
	}

	void endGame() {
		Debug.Log("done");
	}

	void findWoman() {
		womanFound = true;
	}

	void findVideoTape() {
		videoTapeCollected = true;
		Destroy(videoTape);
	}

	void womanSearch() {
		float sqrDistance = (woman.transform.position - transform.position).sqrMagnitude;
		if (!womanFound && sqrDistance < minSqrDistance) {
			RaycastHit hit;
			if (Physics.Linecast (transform.position, woman.transform.position, out hit)) {
				if (hit.collider.gameObject.name == woman.name) {
						findWoman ();
				}				
			}
		}
	}

	void videoTapeSearch() {
		if(!videoTapeCollected) {
			float sqrDistance = (videoTape.transform.position - transform.position).sqrMagnitude;
			if (sqrDistance < minSqrDistance) {
				RaycastHit hit;
				if (Physics.Linecast (transform.position, videoTape.transform.position, out hit)) {
					if (hit.collider.gameObject.name == videoTape.name && Input.GetKeyDown(actionKey)) {
						findVideoTape();
					}				
				}
			}
		}
	}

	void carSearch() {
		float sqrDistance = (car.transform.position - transform.position).sqrMagnitude;
		if (videoTapeCollected && womanFound && sqrDistance < minSqrDistance) {
			RaycastHit hit;
			if (Physics.Linecast (transform.position, car.transform.position, out hit)) {
				if (hit.collider.gameObject.name == car.name && Input.GetKeyDown(actionKey)) {
						endGame();
				}				
			}
		}
	}
}

