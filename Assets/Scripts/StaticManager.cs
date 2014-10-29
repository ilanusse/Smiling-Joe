using UnityEngine;
using System.Collections;

public class StaticManager : MonoBehaviour {

	private float staticTimer;
	private float shockTimer;
	private bool hasStatic;
	private float randomShockInterval;
	private float shockDurationTimer;
	private bool inShock;

	public Renderer staticRenderer;
	public float minShockInterval;
	public float maxShockInterval;
	public float miniShockDuration;
	public Material staticMaterial;
	public Material miniShockMaterial;
	public JumpScareManager jumpScareManager;
	public AudioClip miniShockAudio;
	public AudioClip staticAudio;
	public VictoryManager victoryManager;

	void Start () {
		setMaterialsAlpha(0f);
		shockDurationTimer = 0f;
		shockTimer = 0f;
		staticTimer = 0f;
		randomShockInterval = Random.Range(minShockInterval, maxShockInterval);
	}
	
	void Update () {
		if (!victoryManager.hasGameEnded ()) {
			shockTimer += Time.deltaTime;
			if (shockTimer >= randomShockInterval) {
				if (!hasStatic) {
					if (shockDurationTimer <= miniShockDuration) {
						startShock ();
					} else {
						stopShock ();
					}
				}
			}
		}	
	}
	private void startShock() {
		if(!inShock) {
			startMiniShockAudio();
		}
		shockDurationTimer += Time.deltaTime;
		staticRenderer.material = miniShockMaterial;
		setMaterialsAlpha(1f);
		inShock = true;
	}
	
	private void stopShock() {
		if(inShock){
			stopMiniShockAudio();
		}
		shockDurationTimer = 0f;
		setMaterialsAlpha(0f);
		inShock = false;
		randomShockInterval = Random.Range(minShockInterval, maxShockInterval);	
		shockTimer = 0f;
	}

	public void increaseStatic(float insightHealthTime) {
		staticTimer += Time.deltaTime; 
		if(!hasStatic) {
			jumpScareManager.playJumpScare();
			startStaticAudio();
			hasStatic = true;
			staticRenderer.material = staticMaterial;
			setMaterialsAlpha(0f);
		}
		setMaterialsAlpha(staticTimer/insightHealthTime);
		animateStatic();
	}

	public void reduceStatic(float insightHealthTime) {
		if(staticTimer > 0) {
			staticTimer -= Time.deltaTime;
			setMaterialsAlpha(staticTimer/insightHealthTime);
			animateStatic();
		} else {
			if(hasStatic){
				hasStatic = false;
				stopStaticAudio();
			}
		}
	}

	public void setMaterialsAlpha (float value) {
		Color color = staticRenderer.material.color;
		color.a = value;
		staticRenderer.material.color = color;
	}
	
	private void animateStatic() {
		float randomX = Random.value;
		float randomY = Random.value;
		staticRenderer.material.mainTextureOffset = new Vector2(randomX, randomY);
	}
	
	public void startMiniShockAudio() {
		audio.volume = 1f;
		audio.clip = miniShockAudio;
		audio.Play();
	}
	
	public void stopMiniShockAudio() {
		audio.Stop();
	}
	
	public void startStaticAudio() {
		audio.volume = 0.2f;
		audio.clip = staticAudio;
		audio.Play();
	}
	
	public void stopStaticAudio() {
		audio.Stop();
	}
}
