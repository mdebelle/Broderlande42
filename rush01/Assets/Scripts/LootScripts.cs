using UnityEngine;
using System.Collections;

public class LootScripts : MonoBehaviour {


	public int 		hp;  // damage for a weapon health or xp fo bonus
	MayaScript		maya;

	// Use this for initialization
	void Start () {

		maya = GameObject.Find("Maya").GetComponent<MayaScript>();

	}

//	void OnTriggerEnter(Collider coll){
//
//		Debug.Log ("Loot");
//
//		if (gameObject.tag == "Bonus" && coll.gameObject.tag == "Maya") {
//			coll.gameObject.GetComponent<MayaScript>().increasehealth(maya.Stats.hpMax * hp / 100);
//			Destroy (gameObject);
//		}
//		// Peut-etre changer le script de collision ennemie/weapon
//	//	if (gameObject.tag == "Weapon" && coll.gameObject.tag == "Enemies") {
//	//		coll.gameObject.GetComponent<MayaScript>().takeDamage(hp);
//	//	}
//		
//	}


	void Awake () {


	}

	void Update () {
	}
}
