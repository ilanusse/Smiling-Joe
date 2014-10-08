using UnityEngine;
using System.Collections;

public class JoeMovement : MonoBehaviour {

	enum State
	{
		Idle,
		FreeRoam,
		Chasing,
		RunningAway
	}

	private bool isVisible = false;
	private float offScreenDot = 0.8f;
	private float maxSqrRange;
	private float minSqrRange;
	private State joeState;

	public GameObject target;
	public float maxRange;
	public float minRange;

	// Use this for initialization
	void Start () {
		minSqrRange = minRange * minRange;
		maxSqrRange = maxRange * maxRange;
	}
	
	// Update is called once per frame
	void Update () {
		joeDecisions();

		switch(joeState) {
		case State.Idle:
			transform.LookAt(target);
			rigidbody.velocity = new Vector3( 0, rigidbody.velocity.y, 0 );
		}
	}

	private void joeDecisions() {
		checkIfVisible();
		float sqrDistance = (target.transform.position - transform.position).sqrMagnitude;
		if (isVisible) {
			if (sqrDistance > maxSqrRange) {
				joeState = State.Chasing;
			} else {
				//DECREASE PLAYER HEALTH
				joeState = State.Idle;
			}
			
		} else {
			if(sqrDistance > minSqrRange) {
				joeState = State.Chasing;
			} else {
				joeState = State.Idle;
			}
		}
	}

	private void checkIfVisible() {
		Vector3 forward = target.transform.forward;
		Vector3 targetPosition = (transform.position - target.transform.position).normalized;
		Vector3 relativePosition = Vector3.Dot (targetPosition, forward);
		isVisible = (relativePosition > offScreenDot) ? true : false;
	}
}
