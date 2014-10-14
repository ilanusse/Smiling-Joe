using UnityEngine;
using System.Collections;

public class FlashlightManager : MonoBehaviour {

	enum FlashlightState {
		off,
		on,
		flickering
	}

	private FlashlightState state;
	private float lightTimer;
	private float flickerTimer;
	private float batteryDecreaseTimer;
	private float maxBattery;
	private float maxLightIntensity;
	private float currentMaxLightIntensity;

	public float minStableLightPeriod;
	public float maxStableLightPeriod;
	public float flickeringRate;
	public float flickerDuration;
	public float batteryLife;
	public float batteryConsumption;
	public float batteryDecreaseRate;
	
	void Start () {
		state = FlashlightState.off;
		lightTimer = 0f;
		flickerTimer = 0f;
		batteryDecreaseTimer = 0f;
		maxBattery = batteryLife;
		maxLightIntensity = light.intensity;
		currentMaxLightIntensity = maxLightIntensity;
	}
	
	void Update () {
		checkInput();
		checkBatteryState();
		switch(state) {
			case FlashlightState.off:
				light.enabled = false;
				break;
			case FlashlightState.on:
				light.enabled = true;
				lightTimer += Time.deltaTime;
				float stableLightPeriod = Random.Range(minStableLightPeriod, maxStableLightPeriod);
				if(lightTimer >= stableLightPeriod) {
					lightTimer = 0f;
					state = FlashlightState.flickering;
				}
				break;
			case FlashlightState.flickering:
				lightTimer += Time.deltaTime;
				flickerTimer += Time.deltaTime;
				if(flickerTimer > flickeringRate) {
					light.intensity = Random.Range(0f, currentMaxLightIntensity * 0.8f);
					flickerTimer = 0f;
				}
				if(lightTimer > flickerDuration) {
					lightTimer = 0f;
					light.intensity = currentMaxLightIntensity;
					state = FlashlightState.on;
				}
				break;
		}
	}
	
	private void checkBatteryState() {
		float decreasingFactor = ((batteryLife/maxBattery) < 0.3f) ? 0.3f : (batteryLife/maxBattery);
		currentMaxLightIntensity = maxLightIntensity * decreasingFactor;
		light.intensity = currentMaxLightIntensity; 
		if(batteryLife <= 0) {
			state = FlashlightState.off;
		}
		if(state == FlashlightState.on || state == FlashlightState.flickering) {
			batteryDecreaseTimer += Time.deltaTime;
			if(batteryDecreaseTimer >= batteryDecreaseRate) {
				batteryLife -= batteryConsumption;
				batteryDecreaseTimer = 0f;
			}
		}
	}

	private void checkInput () {
		if(Input.GetMouseButtonDown(0)) {
			if(state == FlashlightState.off) {
				lightTimer = 0f;
				light.intensity = currentMaxLightIntensity;
				state = FlashlightState.on;
			} else {
				state = FlashlightState.off;
			}	
		}
	}
}
