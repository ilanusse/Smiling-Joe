using UnityEngine;
using System.Collections;

public class MapBuilder : MonoBehaviour {

	public GameObject[] blocks;
	// Use this for initialization
	void Start () {
		randomizeArray();
		blocks[0].transform.position = new Vector3 (0f, 10f, 0f);
		blocks[1].transform.position = new Vector3 (-1000f, 10f, 0f);
		blocks[2].transform.position = new Vector3(0f, 10f, 1000f);
		blocks[3].transform.position = new Vector3(-1000f, 10f, 1000f);
	}
	
	void randomizeArray(){
		for (int i = 0; i < blocks.Length; i++){
			swap(i, Random.Range(i,blocks.Length));
		}
	}
	void swap(int i1, int i2){
		var temp = blocks[i1];
		blocks[i1] = blocks[i2];
		blocks[i2] = temp;
	}
}
