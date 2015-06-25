using UnityEngine;
using System.Collections;

public class LambentScipts : MonoBehaviour {

		
	Animator				animator;

	// Use this for initialization
	void Start () {
	
		animator = GetComponent<Animator>();
		animator.SetBool("idle", false);


	}
	
	// Update is called once per frame
	void Update () {

	}


}
