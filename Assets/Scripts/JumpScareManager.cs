using UnityEngine;
using System.Collections;

public class JumpScareManager : MonoBehaviour {

	public AudioClip[] jumpScares;

	public void playJumpScare() {
		if(!audio.isPlaying) {
			audio.volume = 1f;
			audio.clip = jumpScares[Random.Range(0, jumpScares.Length)];
			audio.Play();
		}
	}
}
