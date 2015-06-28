using UnityEngine;
using System.Collections;

public class LootScripts : MonoBehaviour {


	public int 		hp;  // damage for a weapon health or xp fo bonus


	// Use this for initialization
	void Start () {

	}

	void OntriggerEnter(Collider coll){
		if (gameObject.tag == "Bonus" && coll.gameObject.tag == "Maya") {
			coll.gameObject.GetComponent<MayaScript>().increasehealth(hp);
			Destroy (gameObject);
		}
		// Peut-etre changer le script de collision ennemie/weapon
	//	if (gameObject.tag == "Weapon" && coll.gameObject.tag == "Enemies") {
	//		coll.gameObject.GetComponent<MayaScript>().takeDamage(hp);
	//	}
		
	}


	void Awake () {

		if (gameObject.tag == "Bonus") {
		}

	}

	void Update () {
	}
}
