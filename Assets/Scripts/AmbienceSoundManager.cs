using UnityEngine;

public class AmbienceSoundManager : MonoBehaviour {
	private float randomSoundTimer;
	private float laughTimer;
	private float preciseRandomSoundTrigger;
	private float preciseLaughTrigger;


	public AudioClip[] randomSounds;
	public AudioClip[] joeLaughs;
	public float minSoundInterval;
	public float maxSoundInterval;
	public float minLaughInterval;
	public float maxLaughInterval;

	// Use this for initialization
	void Start () {
		laughTimer = 0f;
		randomSoundTimer = 0f;
		preciseRandomSoundTrigger = Random.Range(0, minSoundInterval);
		preciseLaughTrigger = Random.Range(0, minLaughInterval);
	}
	
	// Update is called once per frame
	void Update () {
		laughTimer += Time.deltaTime;
		randomSoundTimer += Time.deltaTime;
		playLaugh();
		playRandomSound();
	}
	
	private void playLaugh() {
		if(laughTimer >= preciseLaughTrigger && !audio.isPlaying) {
			audio.clip = joeLaughs[Random.Range(0, joeLaughs.Length)];
			audio.Play();
			laughTimer = 0f;
			preciseLaughTrigger = Random.Range(minLaughInterval, maxLaughInterval);
		}
	}
	
	private void playRandomSound() {
		if(randomSoundTimer >= preciseRandomSoundTrigger && !audio.isPlaying) {
			audio.clip = randomSounds[Random.Range(0, randomSounds.Length)];
			audio.Play();
			randomSoundTimer = 0f;
			preciseRandomSoundTrigger = Random.Range(minSoundInterval, maxSoundInterval);
		}
	}
}
