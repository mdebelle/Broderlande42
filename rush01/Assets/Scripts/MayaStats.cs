using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MayaStats : MonoBehaviour {
	public int currentXP = 0;
	public int level = 1;
	public int xpToNextLvl = 20;
	public int force = 1;
	public int agi = 1;
	public int con = 1;
	public float prec = 60.0f;
	public int degatsMin = 1;
	public int degatsMax = 2;
	public int hpMax = 100;
	public int hp = 100;

	public Text levelInfos;
	public Text forInfos;
	public Text agiInfos;
	public Text conInfos;

	void Start ()
	{
		MayaScript.instance.healthBar.maxValue = hpMax;
		MayaScript.instance.xpBar.maxValue = xpToNextLvl;
		MayaScript.instance.healthBar.value = hp;
		MayaScript.instance.xpBar.value = currentXP;
		levelInfos = levelInfos.GetComponent<Text>();
		forInfos = forInfos.GetComponent<Text>();
		agiInfos = agiInfos.GetComponent<Text>();
		conInfos = conInfos.GetComponent<Text>();
		levelInfos.text = "\nLvl. " + level;
	}

	void UpdateInfos()
	{
		forInfos.text = "FOR " + force;
		agiInfos.text = "AGI " + agi;
		conInfos.text = "CON " + con;
	}

	void LevelUp ()
	{
		level++;
		Debug.Log("Level up ! Current level: " + level);
		int tmp = currentXP + (currentXP * 25 / 100);
		currentXP -= xpToNextLvl;
		xpToNextLvl = tmp;
		hpMax = hpMax + (level * 5);
		hp = hpMax;
		MayaScript.instance.healthBar.maxValue = hpMax;
		MayaScript.instance.xpBar.maxValue = xpToNextLvl;
		levelInfos.text = "\nLvl. " + level;
	}

	void Update ()
	{
		UpdateInfos();
		if (Input.GetKeyDown(KeyCode.L)) {
			currentXP = currentXP + (xpToNextLvl - currentXP);
		}
		if (currentXP >= xpToNextLvl) {
			LevelUp();
		}
	}
}
