using UnityEngine;
using System.Collections;

public class MapBuilder : MonoBehaviour {

	public GameObject block;
	public GameObject block2;
	// Use this for initialization
	void Start () {
		block.transform.position = new Vector3 (0f, 10f, 0f);
		block2.transform.position = new Vector3 (-1000f, 10f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
