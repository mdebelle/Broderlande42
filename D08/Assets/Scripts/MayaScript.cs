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
					attacked = false;
				}
				else {
					clickattack = false;
				}

			}
		}
//		Debug.Log ("hitpos" +  hitInfo.transform.position);
		
		if (Input.GetMouseButtonUp (0) && attacked == true) {
			Debug.Log ("up");
			clickattack = false;
			if (Time.time - timetoattack > 2f) {
				animator.SetBool ("attack", false);
				attacked = false;
			}
		}

		if (clickattack == true) {
			Debug.Log ("aie");
			MayaAttack (hitInfo.collider);
		} else {
			if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
				animator.SetBool ("run", false);
			} else {
				animator.SetBool ("run", true);	
			}
		}
	}

	void MayaAttack(Collider coll) {
		
		agent.destination = hitInfo.point;
		dist = Mathf.Abs (Vector3.Distance (transform.position, hitInfo.transform.position));
		if (dist > 1.5f) {
			animator.SetBool ("run", true);
		} else {
			animator.SetBool ("run", false);
			if (hitInfo.collider.GetComponent<Enemies> ().lifepoint == 1) {
				clickattack = false;
			}
			hitInfo.collider.GetComponent<Enemies> ().takeDamage ();
			animator.SetBool ("attack", true);
			attacked = true;
		}

	}

	
	void OnAnimatorMove ()
	{
		transform.position = agent.nextPosition;
	}

}
