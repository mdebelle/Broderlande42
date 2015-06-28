using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour {

	
	public List<Enemies>	Zombies = new List<Enemies>();
	Enemies					Clone;
	public Enemies			MidBoss;

	float					timetoborn;
	bool					newone;
	public ParticleSystem	Born;
	bool					smoke;

	public int				numberofEnnemies = 10;
	int						numberofSpawn = 0;

	MayaScript maya;


	// Use this for initialization
	void Start () {
		newone = true;
		smoke = true;
		maya = MayaScript.instance;
	}

	// Update is called once per frame
	void Update () {

		if (!Clone && newone == false) {
			timetoborn = Time.time;
			newone = true;
			smoke = true;
		}


		if (smoke == true && newone == true && Time.time - timetoborn > 2f) {
			if (Born)
				Born.Play ();
			smoke = false;
		}

		if (newone == true && Time.time - timetoborn > 4f) {
			if (numberofSpawn < numberofEnnemies) {
				if (maya.Stats.level < 10)
					Clone = Instantiate (Zombies[Random.Range(0,1)], transform.position, Quaternion.identity) as Enemies;
				else
					Clone = Instantiate (Zombies[Random.Range(0,2)], transform.position, Quaternion.identity) as Enemies;
				numberofSpawn++;
			} else if (numberofSpawn == numberofEnnemies) {
				Clone = Instantiate (MidBoss, transform.position, Quaternion.identity) as Enemies;
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
