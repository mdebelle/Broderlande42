using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MayaStats : MonoBehaviour {
	public int currentXP = 0;
	public int level = 1;
	public int xpToNextLvl;
	public int force = 1;
	public int agi = 1;
	public int con = 1;
	public float prec = 60.0f;
	public int degatsMin = 1;
	public int degatsMax = 2;
	public int hpMax = 10;
	public int hp;
	public int skillPoints = 0;

	public Text levelInfos;
	public Text forInfos;
	public Text agiInfos;
	public Text conInfos;
	public Text statPointsInfos;

	public int	statPoints = 0;

	int nextLevelFormula()
	{
		return (5 * level * level * level / 4);
	}

	void Start ()
	{
		hp = hpMax;
		xpToNextLvl = nextLevelFormula();
		MayaScript.instance.healthBar.maxValue = hpMax;
		MayaScript.instance.xpBar.maxValue = xpToNextLvl;
		MayaScript.instance.healthBar.value = hp;
		MayaScript.instance.xpBar.value = currentXP;
		levelInfos = levelInfos.GetComponent<Text>();
		forInfos = forInfos.GetComponent<Text>();
		agiInfos = agiInfos.GetComponent<Text>();
		conInfos = conInfos.GetComponent<Text>();
		statPointsInfos = statPointsInfos.GetComponent<Text>();
		levelInfos.text = "  " + level.ToString();
	}

	void UpdateInfos()
	{
		forInfos.text = force.ToString();
		agiInfos.text = agi.ToString();
		conInfos.text = con.ToString();
	}

	void LevelUp ()
	{
		level++;
		statPoints += 5;
		Debug.Log("Level up ! Current level: " + level);
		currentXP -= xpToNextLvl;
		xpToNextLvl = nextLevelFormula();
		hpMax = hpMax + (level * 5);
		hp = hpMax;
		MayaScript.instance.healthBar.maxValue = hpMax;
		MayaScript.instance.xpBar.maxValue = xpToNextLvl;
		levelInfos.text = "  " + level.ToString();
		statPointsInfos.text = "  " + statPoints.ToString();
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
