using UnityEngine;
using System.Collections;

public class MayaScript : MonoBehaviour {


	CharacterController 	cc;
	Animator				animator;
	
	private NavMeshAgent	agent;
	RaycastHit				hitInfo = new RaycastHit();


	float					dist;
	bool					clickattack;
	float					timetoattack;
	bool 					attacked;
	string					EnnemyName;
	GameObject				TmpEnemy;

	void Start () {

		cc 			= GetComponent<CharacterController> ();
		animator	= GetComponent<Animator>();
		agent		= GetComponent<NavMeshAgent>();

		lifepoint	= 100;

		clickattack = false;

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
			ItIsTimeToDead ();

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray.origin, ray.direction, out hitInfo)) {
				agent.destination = hitInfo.point;
				if (hitInfo.collider.tag == "Enemy") {
					clickattack = true;
					TmpEnemy = GameObject.Find (hitInfo.collider.name);
					animator.SetBool ("attack", true);
				}
				else {
					animator.SetBool ("attack", false);
					clickattack = false;
				}

			}
		}		
	
		if (clickattack == true)
			MayaAttack (hitInfo.collider);

		if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
			animator.SetBool ("run", false);
		} else {
			animator.SetBool ("run", true);	
		}
	}


	void MayaAttack(Collider coll) {

		dist = Mathf.Abs (Vector3.Distance (transform.position, hitInfo.transform.position));
		if (dist > 1.5f) {
			agent.destination = hitInfo.point;
			animator.SetBool ("run", true);
			animator.SetBool ("attack", false);
		} else {
			agent.destination = transform.position;
			animator.SetBool ("run", false);
			animator.SetBool ("attack", true);
		}
		if (hitInfo.collider.GetComponent<Enemies> ().lifepoint == 0) {
			clickattack = false;
			animator.SetBool ("attack", false);

		}
	}
	
	void OnAnimatorMove () {
		transform.position = agent.nextPosition;
	}

}
