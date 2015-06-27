using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemies : MonoBehaviour {

	Animator				animator;

	private NavMeshAgent	agent;

	private GameObject		Maya;
	public int 				lifepoint;
	float					timetodead;
	bool					attack = false;
	float					timetoattack;

	public List<LootScripts>		loots = new List<LootScripts> ();

	float					distToMaya;

	void Start () {
		Maya = GameObject.Find("Maya");
		animator = GetComponent<Animator>();
		animator.SetBool("idle", false);
		agent = GetComponent<NavMeshAgent>();
		lifepoint = 3;
	}

	void Awake () {
		animator = GetComponent<Animator>();
		animator.SetBool("idle", false);
		agent = GetComponent<NavMeshAgent>();
		lifepoint = 3;
	}

	void Update () {

		if (lifepoint == 0) {
			animator.SetBool ("dead", true);
			animator.SetBool ("idle", false);
			animator.SetBool ("run", false);
			animator.SetBool ("attack", false);

			timetodead = Time.time;

			lifepoint--;
		} else if (lifepoint < 0 && Time.time - timetodead > 3f) {
			Debug.Log ("Dead" + loots.Count);
			Instantiate(loots[Random.Range(0,loots.Count)], transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

		if (lifepoint > 0) {
			distToMaya = Mathf.Abs(Vector3.Distance(Maya.transform.position, transform.position));
			if (distToMaya < 8f && distToMaya >= 1.5f) {
				agent.destination = Maya.transform.position;
				animator.SetBool ("attack", false);
				animator.SetBool ("run", true);
			} else {
				animator.SetBool ("run", false);
				agent.destination = transform.position;
				if (distToMaya < 1.5f) {
					animator.SetBool ("attack", true);
				}
			}
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.tag == "Sword")
			lifepoint--;
	}

	void OnAnimatorMove ()
	{
		transform.position = agent.nextPosition;
	}
}
