using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TipManager : MonoBehaviour {

	public float duration;
	public float fadeDuration;
	public Text tip;

	private float durationTimer;
	private float fadeDurationTimer;
	private bool isActive;

	void Start() {
		fadeDurationTimer = 0.0f;
		isActive = false;
		durationTimer = 0.0f;
		showTip ("Find Laura!");
	}

	void Update() {
		if (isActive) {
			if (fadeDurationTimer <= fadeDuration) {
				fadeDurationTimer += Time.deltaTime;
				setTextAlpha (fadeDurationTimer / fadeDuration);
			} else {
				durationTimer += Time.deltaTime;
				if (durationTimer > duration) {
					isActive = false;
				}
			}
		} else {
			if (fadeDurationTimer > 0.0f) {
				fadeDurationTimer -= Time.deltaTime;
				setTextAlpha (fadeDurationTimer / fadeDuration);
			}
		}
	}

	public void showTip(string s) {
		tip.text = s;
		isActive = true;
		fadeDurationTimer = 0.0f;
		durationTimer = 0.0f;
	}

	public void setTextAlpha (float value) {
		Color color = tip.color;
		color.a = value;
		tip.color = color;
	}
}
