using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	[Header("Statistics")] 
	[SerializeField] private int bulletDamage = 10;
	[SerializeField] private float bulletSpeed = 20f;
	[SerializeField] private float bulletMaxLifeTime = 5f;

	[Header("FX")]
	[SerializeField] private GameObject explosionPrefab;

	private Rigidbody _rb = null;

	private GameObject _caster = null;

	private int _damage = 0;
	private float _bulletPower = 1f;

	protected void Start()
	{
		_rb = GetComponent<Rigidbody>();

		if (_rb == null)
		{
			Debug.LogError("Failed to get Rigidbody from bullet");
		}
		else
		{
			_rb.linearVelocity = transform.forward * bulletSpeed * _bulletPower;
		}

		Debug.Log($"Bullet, speed={bulletSpeed} power={_bulletPower}, damage={_damage}");

		StartCoroutine(LifeTimeCoroutine());
	}

	IEnumerator LifeTimeCoroutine()
	{
		yield return new WaitForSeconds(bulletMaxLifeTime);

		Destroy(gameObject);
	}

	protected void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == _caster)
		{
			return;
		}
		
		if (other.gameObject.TryGetComponent<Destroyable>(out Destroyable destroyable))
		{
			destroyable.TakeHit();
		}
		else if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
		{
			player.TakeDamage(_damage);
		}

		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void SetCaster(GameObject caster)
	{
		_caster = caster;
	}

	public void SetPower(float power)
	{
		_bulletPower = power;
	}

	public void SetDamage(int damage)
	{
		_damage = damage;
	}
}
