using UnityEngine;
using System.Collections;

public class StaticManager : MonoBehaviour {

	private Renderer staticRenderer;
	private float staticTimer;
	private float shockTimer;
	private bool hasStatic;
	private float randomShockInterval;
	private float shockDurationTimer;
	private bool inShock;

	public float minShockInterval;
	public float maxShockInterval;
	public float miniShockDuration;
	public Material staticMaterial;
	public Material miniShockMaterial;

	void Start () {
		staticRenderer = renderer;
		setMaterialsAlpha(0f);
		shockDurationTimer = 0f;
		shockTimer = 0f;
		staticTimer = 0f;
		randomShockInterval = Random.Range(minShockInterval, maxShockInterval);
	}
	
	void Update () {
		shockTimer += Time.deltaTime;
		if( shockTimer >= randomShockInterval) {
			if(!hasStatic) {
				if(shockDurationTimer <= miniShockDuration){
					startShock();
				} else {
					stopShock();
				}
			}
		}
	}	

	private void startShock() {
		shockDurationTimer += Time.deltaTime;
		staticRenderer.material = miniShockMaterial;
		setMaterialsAlpha(1f);
		inShock = true;
	}
	
	private void stopShock() {
		shockDurationTimer = 0f;
		setMaterialsAlpha(0f);
		inShock = false;
		randomShockInterval = Random.Range(minShockInterval, maxShockInterval);	
		shockTimer = 0f;
	}

	public void increaseStatic(float insightHealthTime) {
		staticTimer += Time.deltaTime;
		if(!hasStatic) {
			hasStatic = true;
			staticRenderer.material = staticMaterial;
			setMaterialsAlpha(0f);
		}
		setMaterialsAlpha(staticTimer/insightHealthTime);
	}

	public void reduceStatic(float insightHealthTime) {
		if(staticTimer > 0) {
			staticTimer -= Time.deltaTime;
			setMaterialsAlpha(staticTimer/insightHealthTime);
		} else {
			hasStatic = false;
		}
	}

	public void setMaterialsAlpha (float value) {
		Color color = staticRenderer.material.color;
		color.a = value;
		staticRenderer.material.color = color;
	}
}
