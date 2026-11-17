using UnityEngine;

public class Enemy
{
	private string _name = "Default Name";

	private int _maxHealth = 1;
	private int _health = 1;

	private int _xp = 0;

	private bool _isDead = false;

	public string Name
	{
		get { return _name; }
	}

	public int Health
	{
		get { return _health; }
	}

	public int MaxHealth
	{
		get { return _maxHealth; }
	}

	public int Xp
	{
		get { return _xp; }
	}

	public bool IsDead
	{
		get { return _isDead; }
	}

	public Enemy(EnemyData data)
	{
		_name = data.Label;

		_maxHealth = data.Health;
		_health = _maxHealth;

		_xp = data.Xp;
	}

	public void TakeDamage(int amount)
	{
		_health -= amount;

		if (_health <= 0)
		{
			_health = 0;
			_isDead = true;
		}
	}
}
