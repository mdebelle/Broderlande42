using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEngineScripe : MonoBehaviour {

	public List<SpawnerScript>	Spawn = new List<SpawnerScript>();
	
	int 						currentachivement;
	int 						totalachievement;
	public int					Level;
	public int					totalLevel;

	// Use this for initialization
	void Start () {
		totalachievement = Spawn.Count;
	}
	
	// Update is called once per frame
	void Update () {
	

		if (currentachivement == totalachievement) {
			if (Level < totalLevel)
				Application.LoadLevel("Level "+ Level );
			else {
				Application.LoadLevel("Intro");
			}
		}
		else 
			currentachivement = 0;

		for (int i = 0; i < Spawn.Count; i++){
			if (!Spawn[i].isActiveAndEnabled){
				currentachivement++;
			}
		}


	}
}
