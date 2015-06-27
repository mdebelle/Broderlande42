using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class ScaleParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<ParticleSystem>().startSize = transform.lossyScale.magnitude;
	}
}
