using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour {


	public List<Enemies>	Zombies = new List<Enemies>();
	Enemies			Clone;

	float					timetoborn;
	bool					newone;

	// Use this for initialization
	void Start () {
		newone = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (!Clone && newone == false) {
			timetoborn = Time.time;
			newone = true;
		}


		if (newone == true && Time.time - timetoborn > 2f){
			Clone  = Instantiate (Zombies[Random.Range(0,2)], transform.position, Quaternion.identity) as Enemies;
			Clone.gameObject.SetActive(true);
			newone = false;
		}
	}
}
