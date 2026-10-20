using UnityEngine;
using UnityEngine.UI;
// using System.Collections;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Image fillImage;

	private int _maxHealth = 1;
	private int _health = 1;

	private void UpdateFillValue()
	{
		fillImage.fillAmount = (float) _health / (float) _maxHealth;
	}

	public void SetValues(int health, int maxHealth)
	{
		_maxHealth = maxHealth;
		_health = health;

		UpdateFillValue();
	}

	public void SetHealth(int health)
	{
		_health = health;

		UpdateFillValue();
	}

	public void SetMaxHealth(int maxHealth)
	{
		_maxHealth = maxHealth;

		UpdateFillValue();
	}
}
