using UnityEngine;
using System.Collections;

public class ResetCollider : MonoBehaviour {
	
	void Start () {
		collider.enabled = false;
		collider.enabled = true;
		GetComponent<TerrainCollider>().enabled = false;
		GetComponent<TerrainCollider>().enabled = true;
		GetComponent<TerrainCollider>().active = false;
		GetComponent<TerrainCollider>().active = true;
	}
}
