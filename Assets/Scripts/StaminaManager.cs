using UnityEngine;
using System.Collections;

public class StaminaManager : MonoBehaviour {

	private bool hasStamina;
	private float recoveryTimer;
	private float maxStamina;
	
	public float stamina;
	public AudioClip breathless;
	public float minRecoveryTime;
	
	// Use this for initialization
	void Start () {
		hasStamina = true;
		recoveryTimer = 0f;
		maxStamina = stamina;
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasStamina) {
			recoveryTimer += Time.deltaTime;
		}
		checkForStamina();
	}
	
	public bool getHasStamina() {
		return hasStamina;
	}
	
	public void decreaseStamina() {
		stamina -= Time.deltaTime;
	}
	
	public void increaseStamina() {
		if(stamina < maxStamina) {
			stamina += Time.deltaTime;
		}
	} 
	
	private void checkForStamina() {
		if(stamina <= 0f) {
			playBreathless();
			hasStamina = false;
		}
		if(!hasStamina && recoveryTimer >= minRecoveryTime) {
			hasStamina = true;
			recoveryTimer = 0f;
		}
	}
	
	private void playBreathless() {
		audio.clip = breathless;
		audio.volume = 0.2f;
		if(!audio.isPlaying) {
			audio.Play();
		}
	}
}
