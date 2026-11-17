using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
	[SerializeField] private TMP_Text nameLabel;
	[SerializeField] private TMP_Text healthLabel;
	[SerializeField] private TMP_Text xpLabel;

	[SerializeField] private Image healthbarFill;

	protected void Awake()
	{
		if (nameLabel     == null ||
		    healthLabel   == null ||
		    xpLabel       == null ||
		    healthbarFill == null )
		{
			Debug.LogError("Enemy UI references are not correctly set!");
		}
	}

	public void Set(Enemy enemy)
	{
		// TODO ...
		nameLabel.text = enemy.Name;
		healthLabel.text = $"{enemy.Health} HP";
	}

	public void UpdateHealth(int health)
	{
		// TODO
	}
}
