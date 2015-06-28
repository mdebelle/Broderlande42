using UnityEngine;
using System.Collections;

public class LootScripts : MonoBehaviour {




	// Use this for initialization
	void Start () {

		if (gameObject.tag == "Weapon") {
			transform.position += new Vector3(0,1f,0);
		}

	}
	
	void Awake () {

	}
	// Update is called once per frame
	void Update () {
	
	}
}
