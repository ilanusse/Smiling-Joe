using UnityEngine;
using System.Collections;

public class JoeManager : MonoBehaviour {

	enum JoeState
	{
		Idle,
		Chasing
	}
	
	public bool isVisible = false;
	private float offScreenDot = 0.75f;
	private float maxSqrRange;
	private float minSqrRange;
	private JoeState joeState;
	private Transform joeTransform;
	private Vector3 joeVelocity;
	private Rigidbody joeRigidBody;

	public float telepTime = 30f;
	public float movementSpeed;
	public float difficultyMultiplier = 1.2f;
	public float rotationSpeed;
	public float shoulderMultiplier;
	public Transform target;
	public float maxRange;
	public float minRange;
	public float rayDistance;
	public GameObject[] floorBlocks;
	public HealthManager playerHealth;
	public StaticManager staticManager;

	void Start () {
		minSqrRange = minRange * minRange;
		maxSqrRange = maxRange * maxRange;
		joeTransform = transform; 
		joeRigidBody = rigidbody; 
		if(Difficulty.level.Equals("hard")) {
			movementSpeed = movementSpeed * difficultyMultiplier;
			rotationSpeed = rotationSpeed * difficultyMultiplier;
			telepTime = telepTime / difficultyMultiplier;
		}
		InvokeRepeating( "teleportJoe", 40f, telepTime );
	}

	void Update () {
		joeDecisions();
		switch(joeState) {
			case JoeState.Idle:

				joeVelocity = Vector3.zero;
				joeTransform.LookAt(target);
				break;
			case JoeState.Chasing:
				moving((target.position - joeTransform.position).normalized);
				break;
		}
	}

	private void teleportJoe() {
		checkIfVisible();
		if(!isVisible)  {
			float sqrDistance = (target.position - transform.position).sqrMagnitude;
			if(sqrDistance > maxSqrRange) {
				float teleportDistance = maxRange - 5f;
				int randDir = Random.Range(0,2);
				if(randDir == 0) {
					randDir = -1;
				}
				Vector3 terrainPosCheck = target.position + (randDir * target.right * teleportDistance);
				terrainPosCheck.y = 1000f;

				RaycastHit hit;
				if(Physics.Raycast(terrainPosCheck, Vector3.down, out hit, Mathf.Infinity)) {
					foreach(GameObject floor in floorBlocks) {
						if(hit.collider.gameObject.name == floor.name) {
							Debug.Log ("teleportando a " + hit.collider.gameObject.name); 
							joeTransform.position = hit.point + new Vector3(0f, 0.25f, 0f);
						}
					}

				}
			}	
		}
	}

	private void moving(Vector3 lookDir) {
		RaycastHit hit;

		Vector3 leftShoulder = joeTransform.position - (joeTransform.right * shoulderMultiplier);
		Vector3 rightShoulder = joeTransform.position + (joeTransform.right * shoulderMultiplier);

		if(Physics.Raycast(leftShoulder, joeTransform.forward, out hit, rayDistance)) {
			bool floorCollide = false;
			foreach(GameObject floor in floorBlocks) { 
				if(hit.collider.gameObject.name == floor.name) {
 
					floorCollide = true;
				}
			}
			if(!floorCollide) { 
				Debug.DrawLine( leftShoulder, hit.point, Color.red );
				lookDir += hit.normal * 20f;
			}
		} else if(Physics.Raycast(rightShoulder, joeTransform.forward, out hit, rayDistance)) {
			bool floorCollide = false;
			foreach(GameObject floor in floorBlocks) { 
				if(hit.collider.gameObject.name == floor.name) {
					floorCollide = true;
				}
			}
			if(!floorCollide) { 
				Debug.DrawLine( rightShoulder, hit.point, Color.red );
				lookDir += hit.normal * 20f;
			}
		} else {
			Debug.DrawRay( leftShoulder, joeTransform.forward * rayDistance, Color.yellow );
			Debug.DrawRay( rightShoulder, joeTransform.forward * rayDistance, Color.yellow );
		}
		if ( joeRigidBody.velocity.sqrMagnitude < 1.75 )
		{
			lookDir += joeTransform.right * 20f;
		}
		Quaternion lookRot = Quaternion.LookRotation(lookDir);
		joeTransform.rotation = Quaternion.Slerp(joeTransform.rotation, lookRot, rotationSpeed * Time.deltaTime);
		joeVelocity = joeTransform.forward * movementSpeed;
	}

	void FixedUpdate() {
		float y = joeRigidBody.velocity.y;
		joeRigidBody.velocity = new Vector3(joeVelocity.x, y, joeVelocity.z);
	}
	
	private void joeDecisions() {
		checkIfVisible();
		float sqrDistance = (target.position - transform.position).sqrMagnitude;
		if (isVisible) {
			if (sqrDistance > maxSqrRange) {
				joeState = JoeState.Chasing;
			} else {
				RaycastHit hit;
				if (Physics.Linecast(joeTransform.position, target.position, out hit)) {
					if(hit.collider.gameObject.name == target.name ) {
						playerHealth.decreaseHealth(); 
						staticManager.increaseStatic(playerHealth.inSightHealthTime);
						joeState = JoeState.Idle;
					} else {
						joeState = JoeState.Chasing;
					}
				}
			}
			
		} else {
			staticManager.reduceStatic(playerHealth.inSightHealthTime);
			if(sqrDistance > minSqrRange) {
				joeState = JoeState.Chasing;
			} else {
				joeState = JoeState.Idle;
			}
		}
	}

	private void checkIfVisible() {
		Vector3 forward = target.forward;
		Vector3 targetPosition = (transform.position - target.position).normalized;
		float relativePosition = Vector3.Dot (targetPosition, forward);
		isVisible = (relativePosition > offScreenDot) ? true : false;
	}
}
