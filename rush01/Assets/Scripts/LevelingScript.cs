using UnityEngine;
using System.Collections;

public class LevelingScript : MonoBehaviour {
	public static LevelingScript instance;

	public int currentXP = 0;
	public int currentLevel = 1;
	public int xpToNextLvl = 20;
	public int force = 1;
	public int agi = 1;
	public int con = 1;
	public int degatsMin = 1;
	public int degatsMax = 2;
	public int hpMax = 30;

	void Start ()
	{
		instance = this;
	}

	void LevelUp ()
	{
		currentLevel++;
		int tmp = currentXP + (currentXP * 25 / 100);
		currentXP -= xpToNextLvl;
		xpToNextLvl = tmp;
		force += Random.Range(0, 3);
		agi += 1;
		con += Random.Range(1, 2);
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.L))
			currentXP = currentXP + (xpToNextLvl - currentXP);
		if (currentXP >= xpToNextLvl)
			LevelUp();
		Debug.Log("Level: " + currentLevel);
		Debug.Log("XP: " + currentXP);
		Debug.Log("Next level: " + xpToNextLvl);
		Debug.Log("FOR: " + force);
		Debug.Log("AGI: " + agi);
		Debug.Log("CON: " + con);
	}
}
