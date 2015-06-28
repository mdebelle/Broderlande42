using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MayaScript : MonoBehaviour {
	NavMeshAgent			agent;
	RaycastHit				hit = new RaycastHit();

	float					distToTarget;
	float					ggtime = 12.5f;
	BoxCollider				weaponHbox;
	bool					targetLocked;
	bool					hasToAttack;
	bool					hasTarget = false;

	public AudioSource		ALevelUp;
	public AudioSource		AGgstyle;
	public AudioSource		AMainMusic;

	public static MayaScript	instance;
	public Animator				animator;
	public MayaStats			Stats;

	public GameObject			Weapon;

	public Slider				healthBar;
	public Slider				xpBar;
	
	List<Weapons>				LWeapons = new List<Weapons>();


	void Start () {
		instance	= this;
		agent		= GetComponent<NavMeshAgent>();
		Weapon = GameObject.Find ("W_SwordGame");
		weaponHbox	= Weapon.GetComponent<BoxCollider>();

		healthBar = healthBar.GetComponent<Slider>();
		xpBar = xpBar.GetComponent<Slider>();
	}

	public void increasehealth(int value){

		Debug.Log ("Je vais mieux");

		Stats.hp += value;
		if (Stats.hp > Stats.hpMax) {
			Stats.hp = Stats.hpMax;
		}
	}

	public void takeDamage()
	{
		if (Random.Range(0, 100) <= (100 - 2 * Stats.agi))
		{
			int dmg = Stats.level - (Stats.con / 2);
			Stats.hp -= dmg;
		}
	}

	void OnTriggerEnter (Collider coll) {
		Debug.Log (coll.name);
		if (coll.tag == "Hangar") {
			Camera.main.transform.localPosition -= new Vector3 (0f, 2f,0f);
		}

		if (coll.tag == "Bonus") {
			Debug.Log ("je vais mieux");
			increasehealth(coll.GetComponent<LootScripts>().hp * Stats.hpMax / 100);
			Destroy(coll.gameObject);
		}

		if (coll.tag == "Weapon") {
			LWeapons.Add (coll.gameObject.GetComponent<Weapons>());

		}


	}
	void OnTriggerExit (Collider coll) {
		if (coll.tag == "Hangar") {
			Camera.main.transform.localPosition += new Vector3 (0f, 2f,0f);
		}
	}

	IEnumerator MayaAttack()
	{
		distToTarget = Mathf.Abs (Vector3.Distance (transform.position, hit.transform.position));
		if (distToTarget <= 1.5f)
		{
			agent.destination = transform.position;
			animator.SetBool("run", false);
			animator.SetBool("attack", true);
			weaponHbox.enabled = true;
			yield return new WaitForSeconds(0.5f);
			hasToAttack = false;
		}
	}

	IEnumerator MayaIsDying()
	{
		animator.SetBool("dead", true);
		yield return new WaitForSeconds(4.0f);
		Application.LoadLevel("skills");
	}

	IEnumerator gangnamStyle()
	{
		animator.SetBool ("ggstyle", true);
		AMainMusic.Stop();
		AGgstyle.Play ();
		yield return new WaitForSeconds(ggtime);
		AGgstyle.Stop ();
		AMainMusic.Play();
		animator.SetBool ("ggstyle", false);
	}

	void Update () {
		healthBar.value = Stats.hp;
		xpBar.value = Stats.currentXP;

		if (Stats.hp <= 0) {
			agent.destination = transform.position;
			StartCoroutine(MayaIsDying());
		}
		else {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
					agent.destination = hit.point;
					if (hit.collider.tag == "Enemy") {
						hasTarget = true;
						hasToAttack = true;
					} else {
						hasTarget = false;
						hasToAttack = false;
						animator.SetBool("attack", false);
						animator.SetBool("run", true);
					}
				}
			}

			targetLocked = Input.GetMouseButton(0);

			if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
				animator.SetBool ("run", false);
			} else {
				animator.SetBool ("run", true);
			}

			if (hasToAttack) {
				StartCoroutine(MayaAttack());
				StopCoroutine(MayaAttack());
			}
			else if (!targetLocked || (hasTarget && hit.collider.gameObject.GetComponent<Enemies>().hp <= 0)) {
				animator.SetBool ("attack", false);
				weaponHbox.enabled = false;
			}

			if (Input.GetKeyDown (KeyCode.G)) {
				StartCoroutine(gangnamStyle());
				StopCoroutine(gangnamStyle());
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			equipMaya();
		}

	}

	void equipMaya (){
	
		if (LWeapons.Count > 1) {
			for (int i = 0; i < LWeapons.Count; i++){
				if (i < LWeapons.Count) {
					if (LWeapons[i].GetComponent<Weapons>().equiped == true) {
						LWeapons[i].GetComponent<Weapons>().equiped = false;
						LWeapons[i + 1].GetComponent<Weapons>().equiped = true;
						if (LWeapons[i + 1].GetComponent<Weapons>().name == "Sabre"){
							GameObject.Find("W_Sabre").SetActive(true);
							GameObject.Find("W_SwordGame").SetActive(false);
							GameObject.Find("W_Longclaw").SetActive(false);
						}
						else if (LWeapons[i + 1].GetComponent<Weapons>().name == "SwordGame"){
							GameObject.Find("W_Sabre").SetActive(false);
							GameObject.Find("W_SwordGame").SetActive(true);
							GameObject.Find("W_Longclaw").SetActive(false);
						}
						else if (LWeapons[i + 1].GetComponent<Weapons>().name == "Longclaw"){
							GameObject.Find("W_Sabre").SetActive(true);
							GameObject.Find("W_SwordGame").SetActive(false);
							GameObject.Find("W_Longclaw").SetActive(false);
						}
						break;
					}
				}
				else {
					LWeapons[i].GetComponent<Weapons>().equiped = false;
					LWeapons[0].GetComponent<Weapons>().equiped = true;
					if (LWeapons[0].GetComponent<Weapons>().name == "Sabre"){
						GameObject.Find("W1").SetActive(true);
						GameObject.Find("W2").SetActive(false);
						GameObject.Find("W3").SetActive(false);
					}
					else if (LWeapons[0].GetComponent<Weapons>().name == "SwordGame"){
						GameObject.Find("W1").SetActive(false);
						GameObject.Find("W2").SetActive(true);
						GameObject.Find("W3").SetActive(false);
					}
					else if (LWeapons[0].GetComponent<Weapons>().name == "Longclaw"){
						GameObject.Find("W1").SetActive(true);
						GameObject.Find("W2").SetActive(false);
						GameObject.Find("W3").SetActive(false);
					}
				}
			}
		}
	}
	
	void OnAnimatorMove () {
		transform.position = agent.nextPosition;
	}
}