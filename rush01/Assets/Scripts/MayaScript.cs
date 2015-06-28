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
	int						xp = 0;
	float					ggtime;
	float					leveluptime;

	public AudioSource		ALevelUp;


	void Start () {

		cc 			= GetComponent<CharacterController> ();
		animator	= GetComponent<Animator>();
		agent		= GetComponent<NavMeshAgent>();

		lifepoint	= 100;

		clickattack = false;
		attacked	= false;
		ggtime = 0f;
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


		if (Time.time - ggtime > 3f)
			animator.SetBool ("ggstyle", false);
		if (Input.GetKeyDown (KeyCode.L)) {
			animator.SetBool ("ggstyle", true);
			ggtime = Time.time;
		}

		if (lifepoint <= 0)
			ItIsTimeToDead ();

		if (Input.GetMouseButtonDown (0) || Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray.origin, ray.direction, out hitInfo)) {
				agent.destination = hitInfo.point;
				if (hitInfo.collider.tag == "Enemy") {
					clickattack = true;
					TmpEnemy = GameObject.Find (hitInfo.collider.name);
					animator.SetBool ("attack", true);
				} else {
					animator.SetBool ("attack", false);
					clickattack = false;
				}

			}
		}

		if (Input.GetMouseButton (0)) {
			Debug.Log ("click");
			animator.SetBool ("click", true);
		} else {
			Debug.Log ("pas click");
			animator.SetBool ("click", false);
		}

		if (clickattack == true)
			MayaAttack (hitInfo.collider);

		if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
			animator.SetBool ("run", false);
		} else {
			animator.SetBool ("run", true);
		}


		if (Time.time - leveluptime > 2f &&  xp > 20) {
			animator.SetBool ("LevelUp", true);
			ALevelUp.Play ();
			leveluptime = Time.time;
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
			if (Input.GetMouseButton (0)){
				animator.SetBool ("click", true);
				animator.SetBool ("attack", true);
			}
			else {
				animator.SetBool ("click", false);
				animator.SetBool ("attack", false);
			}
		}

		if (hitInfo.collider.GetComponent<Enemies> ().lifepoint == 0) {
			clickattack = false;
			xp += 5;
			animator.SetBool ("attack", false);
			Debug.Log (xp);
		}
	}

	void OnAnimatorMove () {
		transform.position = agent.nextPosition;
	}
}