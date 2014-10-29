using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject ambienceMusic;

	public void setDifficultyHard() {
		Difficulty.level = "hard";
	}
	
	public void setDifficultyNormal() {
		Difficulty.level = "normal";
	}
	
	public void exitGame() {
		Application.Quit();
	}

	public void playGame() {
		ambienceMusic.audio.Stop ();
	}
}
