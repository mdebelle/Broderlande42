using UnityEngine;
using System.Collections;

public class LootScripts : MonoBehaviour {


	public int 		hp;  // damage for a weapon health or xp fo bonus
	MayaScript		maya;

	// Use this for initialization
	void Start () {

		maya = GameObject.Find("Maya").GetComponent<MayaScript>();

	}



	void Awake () {


	}

	void Update () {
	}
}
