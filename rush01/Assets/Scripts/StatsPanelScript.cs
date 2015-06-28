using UnityEngine;
using System.Collections;

public class StatsPanelScript : MonoBehaviour {
	bool hidden = false;
	Vector3 hidePos;
	Vector3 showPos;

	void Start () {
		hidePos = transform.position;
		showPos = new Vector3(205.8f, 211.13f, 0.0f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.S)) {
			hidden = !hidden;
		}
		if (!hidden) {
			transform.position = Vector3.MoveTowards(transform.position, hidePos, Time.deltaTime * 2000.0f);
		}
		else {
			transform.position = Vector3.MoveTowards(transform.position, showPos, Time.deltaTime * 2000.0f);
		}
	}
}
