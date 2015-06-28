using UnityEngine;
using System.Collections;

public class IntroScripte : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LaunchGame(){
		Application.LoadLevel ("Level1");
	}

	public void ExitGame(){
		Application.Quit ();
	}

}
