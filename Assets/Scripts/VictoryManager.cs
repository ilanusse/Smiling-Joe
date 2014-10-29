using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
	public GameObject laura;
	public GameObject videoTape;
	public GameObject car;
	public KeyCode actionKey;
	public float minSqrDistance;
	public Renderer finisherRenderer;
	public Material winningMaterial;
	public float fadeDuration;
	public Text endingText;
	public float textFadeDuration;
	public GameObject player;
	public GameObject ambienceMusic;
	public float redirectDuration;
	public AudioClip winningMusic;
	public AudioClip losingMusic;
	public Material losingMaterial;

	private bool videoTapeCollected;
	private bool womanFound;
	private bool gameEnded = false;
	private float fadeTimer;
	private float textFadeTimer;
	private float redirectTimer;

	void Start () {
		womanFound = false;
		videoTapeCollected = false;
		gameEnded = false;
		fadeTimer = 0.0f;
		textFadeTimer = 0.0f;
		redirectTimer = 0.0f;
	}

	void FixedUpdate () {
		if (!gameEnded) {
			womanSearch ();
			videoTapeSearch ();
			carSearch ();
		} else {
			if(fadeTimer < fadeDuration) {
				fadeTimer += Time.deltaTime;
				setMaterialsAlpha (fadeTimer / fadeDuration);
			} else {
				if(textFadeTimer < textFadeDuration) {
					Debug.Log ("text alpha");
					textFadeTimer += Time.deltaTime;
					setTextAlpha (textFadeTimer / textFadeDuration);
				} else {
					redirectTimer += Time.deltaTime;
				}
			}
		}
		if (redirectTimer > redirectDuration) {
			Application.LoadLevel ("menu");
		}
	}

	void endGame() {
		gameEnded = true;
		ambienceMusic.audio.Stop ();
		endingText.text = "THE END";
		finisherRenderer.material = winningMaterial;
		setMaterialsAlpha (fadeTimer / fadeDuration);
		audio.clip = winningMusic;
		audio.Play ();
	}

	void findWoman() {
		womanFound = true;
	}

	void findVideoTape() {
		videoTapeCollected = true;
		Destroy(videoTape);
	}

	void womanSearch() {
		float sqrDistance = (laura.transform.position - player.transform.position).sqrMagnitude;
		if (!womanFound && sqrDistance < minSqrDistance) {
			RaycastHit hit;
			if (Physics.Linecast (player.transform.position, laura.transform.position, out hit)) {
				if (hit.collider.gameObject.name == laura.name) {
						findWoman ();
				}				
			}
		}
	}

	void videoTapeSearch() {
		if(!videoTapeCollected) {
			float sqrDistance = (videoTape.transform.position - player.transform.position).sqrMagnitude;
			if (sqrDistance < minSqrDistance) {
				RaycastHit hit;
				if (Physics.Linecast (player.transform.position, videoTape.transform.position, out hit)) {
					if (hit.collider.gameObject.name == videoTape.name && Input.GetKey(actionKey)) {
						findVideoTape();
					}				
				}
			}
		}
	}

	void carSearch() {
		float sqrDistance = (car.transform.position - player.transform.position).sqrMagnitude;
		if (videoTapeCollected && womanFound && sqrDistance < minSqrDistance) {
			RaycastHit hit;
			if (Physics.Linecast (player.transform.position, car.transform.position, out hit)) {
				if (hit.collider.gameObject.name == car.name && Input.GetKey(actionKey)) {
						endGame();
				}				
			}
		}
	}

	public void setMaterialsAlpha (float value) {
		Color color = finisherRenderer.material.color;
		color.a = value;
		finisherRenderer.material.color = color;
	}

	public void setTextAlpha (float value) {
		Color color = endingText.color;
		color.a = value;
		endingText.color = color;
	}

	public bool hasGameEnded() {
		return gameEnded;
	}

	public void playerDied() {
		gameEnded = true;
		ambienceMusic.audio.Stop ();
		endingText.text = "YOU DIED";
		endingText.color = new Color(0.5568f, 0.2118f, 0.2118f, 0.0f);
		finisherRenderer.material = losingMaterial;
		setMaterialsAlpha (fadeTimer / fadeDuration);
		audio.clip = losingMusic;
		audio.Play ();
	}
}
