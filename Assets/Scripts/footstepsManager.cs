using UnityEngine;
using System.Collections;

public class footstepsManager : MonoBehaviour {
		
	public AudioClip[] footstepSounds;
	
	private float walkAudioSpeed;
	private float runAudioSpeed;
	private float walkAudioTimer;
	private float runAudioTimer;
	private bool isWalking;
	private bool hasStamina;
	private CharacterController characterController;
	private CharacterMotor characterMotor;
	private bool isRunning;

	public VictoryManager victoryManager;
	public StaminaManager staminaManager;
	public float walkSpeed;
	public float runSpeed;
	
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		characterMotor = GetComponent<CharacterMotor>();
		walkAudioSpeed = 0.6f;
		runAudioSpeed = 0.4f;
		walkAudioTimer = 0f;
		runAudioTimer = 0f;
		isWalking = false;
		isRunning = false;
		walkSpeed = 8f;
		runSpeed = 20f;
	}
	
	void Update() {
		if (!victoryManager.hasGameEnded()) {
			hasStamina = staminaManager.getHasStamina ();
			SetSpeed ();
			if (characterController.isGrounded) {
				PlayFootsteps ();
			} else {
				walkAudioTimer = 1000f;
				runAudioTimer = 1000f;
			}
		}
	}
	
	private void SetSpeed() {
		float speed = walkSpeed;
		
		if (characterController.isGrounded) {
		    if ((Input.GetKey("left shift") || Input.GetKey("right shift")) && hasStamina) {
				speed = runSpeed;
				staminaManager.decreaseStamina();
			} else {
				speed = walkSpeed;
				staminaManager.increaseStamina();
			}
		}
		
		characterMotor.movement.maxForwardSpeed = speed;
	}
	
	private void PlayFootsteps()
	{
		if (Input.GetAxis( "Horizontal" ) != 0f || Input.GetAxis( "Vertical" ) != 0f) {
			if ((Input.GetKey( "left shift" ) || Input.GetKey( "right shift" )) && hasStamina) {
				// Running
				isWalking = false;
				isRunning = true;
			} else {
				// Walking
				isWalking = true;
				isRunning = false;
			}
		} else {
			// Stopped
			isWalking = false;
			isRunning = false;
		}
		
		// Play Audio
		if ( isWalking ) {
			if ( walkAudioTimer > walkAudioSpeed ) {
				audio.volume = 0.1f;
				playFootstep();
				walkAudioTimer = 0f;
			}
		} else if ( isRunning ) {
			if ( runAudioTimer > runAudioSpeed ) {
				audio.volume = 0.2f;
				playFootstep();
				runAudioTimer = 0f;
			}
		} else {
			audio.Stop();
		}
		
		// increment timers
		walkAudioTimer += Time.deltaTime;
		runAudioTimer += Time.deltaTime;
	}
	
	private void playFootstep() {
		audio.Stop();
		audio.clip = footstepSounds[ Random.Range( 0, footstepSounds.Length ) ];
		audio.Play();
	}
	
}
