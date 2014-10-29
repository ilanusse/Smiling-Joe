using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour {

	public void setDifficultyHard() {
		Difficulty.level = "hard";
	}
	
	public void setDifficultyNormal() {
		Difficulty.level = "normal";
	}
}
