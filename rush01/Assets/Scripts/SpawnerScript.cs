using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour {


	public List<Enemies>	Zombies = new List<Enemies>();
	Enemies					Clone;

	float					timetoborn;
	bool					newone;
	public ParticleSystem	Born;
	bool					smoke;

	public int				numberofEnnemies = 10;
	int						numberofSpawn = 0;


	// Use this for initialization
	void Start () {
		newone = true;
		smoke = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (!Clone && newone == false) {
			timetoborn = Time.time;
			newone = true;
			smoke = true;
		}


		if (smoke == true && newone == true && Time.time - timetoborn > 2f) {
			Born.Play ();
			smoke = false;
		}

		if (newone == true && Time.time - timetoborn > 4f) {
			if (numberofSpawn < numberofEnnemies) {
				Clone = Instantiate (Zombies[Random.Range(0,2)], transform.position, Quaternion.identity) as Enemies;
				numberofSpawn++;
			} else if (numberofSpawn == numberofEnnemies) {
				Clone = Instantiate (Zombies[2], transform.position, Quaternion.identity) as Enemies;
				numberofSpawn++;
			}
			Clone.gameObject.SetActive(true);
			newone = false;
		}


		if (numberofSpawn > numberofEnnemies) {
			gameObject.SetActive(false);
		}

	}
}
