using UnityEngine;
using System.Collections;

public class MayaScript : MonoBehaviour {


	CharacterController 	cc;

	Animator				animator;
	protected AnimateState	CharacterAnimationState = AnimateState.idle;

	protected enum AnimateState {
		idle,
		run,
		attack,
		dead
	}

	public int health;

	#region NavMesh

	Vector3 				target;
	Ray						ray;
	RaycastHit				hit;
	private NavMeshAgent	agent;
	private NavMeshPath		path;


	RaycastHit hitInfo = new RaycastHit();
	Vector2 smoothDeltaPosition = Vector2.zero;
	Vector2 velocity = Vector2.zero;

	#endregion

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		cc = GetComponent<CharacterController> ();
		agent = GetComponent<NavMeshAgent>();
		path = new NavMeshPath();

		target = transform.position;
		Debug.Log (target);
		health = 100;

	}


	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
				agent.destination = hitInfo.point;
		}

		if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
			animator.SetBool("run", false);
		} else {
			animator.SetBool("run", true);	
		}

		
	//	GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;

	}

	
	void OnAnimatorMove ()
	{
		// Update position to agent position
		transform.position = agent.nextPosition;
	}

}
