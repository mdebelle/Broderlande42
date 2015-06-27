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
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.L)) {
			currentXP = currentXP + (xpToNextLvl - currentXP);
		}
		if (currentXP >= xpToNextLvl) {
			LevelUp();
		}
	}
}
