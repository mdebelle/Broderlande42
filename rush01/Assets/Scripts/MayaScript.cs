using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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


	void Start () {
		instance	= this;
		agent		= GetComponent<NavMeshAgent>();
		weaponHbox	= Weapon.GetComponent<BoxCollider>();

		healthBar = healthBar.GetComponent<Slider>();
		xpBar = xpBar.GetComponent<Slider>();
	}

	public void increasehealth(int value){
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
			StartCoroutine(MayaIsDying());
		}

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
		}
		else if (!targetLocked || (hasTarget && hit.collider.gameObject.GetComponent<Enemies>().hp <= 0)) {
			animator.SetBool ("attack", false);
			weaponHbox.enabled = false;
		}

		if (Input.GetKeyDown (KeyCode.G)) {
			StartCoroutine(gangnamStyle());
		}
	}

	void OnAnimatorMove () {
		transform.position = agent.nextPosition;
	}
}