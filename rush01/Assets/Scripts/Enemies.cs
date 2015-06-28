using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Enemies : MonoBehaviour {

	Animator					animator;

	private NavMeshAgent		agent;

	private string[]			renames = {"Xavier Niel", "Nicolas Sadirac", "Kwame", "Butcher", "Ol", "Thør"};
	private float				distToMaya;
	private MayaScript			Maya;

	public int 					hp;
	public List<LootScripts>	loots = new List<LootScripts> ();
	public AudioSource			AHeart;
	public AudioSource			Aattack;

	public CharacterController	hitbox;
	
	public Text					EName;
	float secsToHit = 1.45f;

	int							level;

	void Awake () {
		Maya = GameObject.Find("Maya").GetComponent<MayaScript>();
		animator = GetComponent<Animator>();
		animator.SetBool("idle", false);
		agent = GetComponent<NavMeshAgent>();
		hp = Maya.Stats.level * 3;
		hitbox = GetComponent<CharacterController>();

		EName.GetComponent<Text>().text = renames[Random.Range(0, 6)];

		level = Maya.Stats.level;

		AHeart.Play ();
	}

	IEnumerator isDying()
	{
		animator.SetBool ("dead", true);
		Debug.Log ("Dead" + loots.Count);
		hitbox.enabled = false;
		yield return new WaitForSeconds(4.0f);
		if (Random.Range(0,8) < loots.Count)
		Instantiate(loots[Random.Range(0,loots.Count)], transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	void Update () {
		if (hp <= 0) {
			StartCoroutine(isDying());
		}

		if (hp > 0) {
			distToMaya = Mathf.Abs(Vector3.Distance(Maya.transform.position, transform.position));
			if (distToMaya < 8f && distToMaya >= 1.5f) {
				agent.destination = Maya.transform.position;
				animator.SetBool ("attack", false);
				animator.SetBool ("run", true);
			} else {
				animator.SetBool ("run", false);
				agent.destination = transform.position;
				if (distToMaya < 1.5f)
					animator.SetBool ("attack", true);
				secsToHit -= Time.smoothDeltaTime;
				if (secsToHit <= 0)
				{
					secsToHit = 1.45f;
					Debug.Log(secsToHit);
					distToMaya = Mathf.Abs(Vector3.Distance(Maya.transform.position, transform.position));
					if (distToMaya < 1.5f) {
						Maya.takeDamage();
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.tag == "Weapon")
		{
			if (Random.Range(0, 100) <= Maya.Stats.prec) {
				Debug.Log("Touché !");
				Aattack.Play ();
				hp -= Maya.Stats.force;
				if (hp <= 0) {
					int xpGain = 42 * level / 7;
					Maya.Stats.currentXP += xpGain;
				}
			}
			else {
				Debug.Log("Missed ...");
			}
		}
	}

	void OnAnimatorMove ()
	{
		transform.position = agent.nextPosition;
	}
}
