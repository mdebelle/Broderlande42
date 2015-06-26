using UnityEngine;
using System.Collections;

public class MayaScript : MonoBehaviour {


	CharacterController 	cc;
	Animator				animator;
	
	private NavMeshAgent	agent;
	RaycastHit				hitInfo = new RaycastHit();


	void Start () {

		cc 			= GetComponent<CharacterController> ();
		animator	= GetComponent<Animator>();
		agent		= GetComponent<NavMeshAgent>();

		lifepoint	= 100;

	}

	#region dead

	public int 				lifepoint;
	float					timetodead;

	void ItIsTimeToDead (){

		if (lifepoint == 0) {
			animator.SetBool ("dead", true);
			timetodead = Time.time;
			lifepoint--;
		} else if (lifepoint < 0 && Time.time - timetodead > 3f) {
			Destroy(gameObject);
		}

	}

	#endregion


	void OnTriggerEnter (Collider coll) {
		Debug.Log (coll.name);
		if (coll.tag == "Hangar") {
			Camera.main.transform.localPosition -= new Vector3 (0f, 2f,0f);
		}
	}
	void OnTriggerExit (Collider coll) {
		if (coll.tag == "Hangar") {
			Camera.main.transform.localPosition += new Vector3 (0f, 2f,0f);
		}
	}
	void Update () {

		if (lifepoint <= 0)
			ItIsTimeToDead();

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
	}

	
	void OnAnimatorMove ()
	{
		transform.position = agent.nextPosition;
	}

}
