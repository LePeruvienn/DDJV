using UnityEngine;

public class Entity : MonoBehaviour
{
	[Header("Statistics")]
	[SerializeField] protected int maxHealth = 100;

	[Header("Movement")]
	[SerializeField] protected float speed = 4f;
	[SerializeField] protected float angularSpeed = 360f;

	[Header("Bullet")]
	[SerializeField] protected Bullet bulletPrefab;
	[SerializeField] protected float bulletHeight = 1f;

	[Header("FX")]
	[SerializeField] protected GameObject characterExplosion;

	protected int _health = 1;
	protected bool _isDead = false;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_health = maxHealth;
		_isDead = false;
	}

	public virtual void ShootBullet()
	{
		Vector3 spawnPosition = transform.position + new Vector3(0, bulletHeight, 0);
		Quaternion spawnRotation = transform.rotation;

		Bullet bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);

		bullet.SetCaster(gameObject);
	}

	public virtual bool ApplyDamage(int value)
	{
		_health -= value;
		
		if (_health <= 0)
		{
			_health = 0;

			Die();

			return true;
		}

		return false;
	}

	public virtual void Die()
	{
		 Instantiate(characterExplosion, transform.position, Quaternion.identity);

		 // Disable object
		 gameObject.SetActive(false);
	}
}
